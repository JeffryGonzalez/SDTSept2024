
using Hr.Api.HiringNewEmployees.Entities;
using Hr.Api.HiringNewEmployees.Models;
using Hr.Api.HiringNewEmployees.Services;
using Marten;

namespace Hr.Api.HiringNewEmployees.Endpoints;

public class HiringRequestsController(TimeProvider clock, IDocumentSession session, IGenerateSlugIdsForEmployees slugGenerator) : ControllerBase
{
    [HttpPost("/departments/IT/hiring-requests")]
    //[Authorize(Policy = "IsHiringManager")]
    public async Task<ActionResult> HireAnEmployee(
        [FromBody] EmployeeHiringRequestModel request,
        [FromServices] EmployeeHiringRequestValidator validator,
        [FromServices] INotifyTheCto http
        )
    {
        var department = "it";
        var validations = await validator.ValidateAsync(request);

        if (!validations.IsValid)
        {
            return BadRequest(validations.ToDictionary());
        }


        var requestId = Guid.NewGuid();
        var sub = User.Identity?.Name;
        var entity = new EmployeeHiringRequestEntity
        {
            Id = requestId,
            ApplicationDate = clock.GetUtcNow(),
            Status = "Hired",
            SubmittedBy = sub!,
            PersonalInformation = new HiringRequestPersonalInformation
            {
                DepartmentAppliedTo = department,
                Name = request.Name
            }

        };
        entity.Links.Add("self",
            $"/departments/{department}/hiring-requests/{requestId}");
        entity.Links.Add("hiring-request:submitted-by", $"/employees/{sub}");
        // since this is an IT person..
        var employeeId = await slugGenerator.GenerateIdForItAsync(request.Name);
        //    "departments:employee": "/departments/{department}/employees/itsmith-bob"
        entity.Links.Add("departments:employee", $"/departments/it/employees/{employeeId}");
        // Save this hiring request somewhere.
        entity.EmployeeId = employeeId;

        var receipt = await http.NotifyCioOfNewItHireAsync(new CioNotificationApiTypes.NewItHiringNotificationRequest { Name = entity.PersonalInformation.Name, WhenHired = clock.GetUtcNow() });
        entity.CioReceipt = receipt.NotificationDeliveryReceipt;
        session.Store(entity);
        await session.SaveChangesAsync();
        var mapper = new EmployeeHiringRequestMapper();
        var response = mapper.ToResponseModel(entity);
        return Ok(response);
    }

    [HttpGet("/departments/{department}/hiring-requests/{requestId:guid}")]
    public async Task<ActionResult> GetEmployeeByDepartmentAsync(string department, Guid requestId)
    {
        var entity = await session.LoadAsync<EmployeeHiringRequestEntity>(requestId);
        if (entity == null)
        {
            return NotFound();
        }
        else
        {
            var mapper = new EmployeeHiringRequestMapper();
            var response = mapper.ToResponseModel(entity);
            return Ok(response);
        }
    }
}




