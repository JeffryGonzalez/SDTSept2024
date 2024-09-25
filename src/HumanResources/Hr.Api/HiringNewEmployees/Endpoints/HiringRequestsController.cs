using Hr.Api.HiringNewEmployees.Entities;
using Hr.Api.HiringNewEmployees.Models;
using Marten;

namespace Hr.Api.HiringNewEmployees.Endpoints;

public class HiringRequestsController(TimeProvider clock, IDocumentSession session) : ControllerBase
{
    [HttpPost("/departments/IT/hiring-requests")]
    public async Task<ActionResult> HireAnEmployee(
        [FromBody] EmployeeHiringRequestModel request,
        [FromServices] EmployeeHiringRequestValidator validator
        )
    {
        var department = "it";
        var validations = await validator.ValidateAsync(request);

        if (!validations.IsValid)
        {
            return BadRequest(validations.ToDictionary());
        }

        var requestId = Guid.NewGuid();
        var entity = new EmployeeHiringRequestEntity
        {
            Id = requestId,
            ApplicationDate = clock.GetUtcNow(),
            Status = "Hired",
            PersonalInformation = new HiringRequestPersonalInformation
            {
                DepartmentAppliedTo = department,
                Name = request.Name
            }

        };
        entity.Links.Add("self",
            $"/departments/{department}/hiring-requests/{requestId}");


        // Save this hiring request somewhere.
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




