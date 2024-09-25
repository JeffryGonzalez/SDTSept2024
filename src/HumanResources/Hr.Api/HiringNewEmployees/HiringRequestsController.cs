using Hr.Api.HiringNewEmployees.Services;

namespace Hr.Api.HiringNewEmployees;

public class HiringRequestsController(TimeProvider clock, IGenerateSlugIdsForEmployees employeeIdGenerator) : ControllerBase
{
    [HttpPost("/departments/IT/hiring-requests")]
    public async Task<ActionResult> HireAnEmployee(
        [FromBody] EmployeeHiringRequestModel request,
        [FromServices] EmployeeHiringRequestValidator validator
        )
    {
        var validations = await validator.ValidateAsync(request);

        if (!validations.IsValid)
        {
            return BadRequest(validations.ToDictionary());
        }
        string id = await employeeIdGenerator.GenerateIdForItAsync(request.Name);
        // if the new employee is in IT, then send a notification to the CIO because he likes to buy them coffee.
        return Ok(new EmployeeHiringRequestResult(id, request.Name, "IT", 182000M, clock.GetUtcNow()));
    }
}




