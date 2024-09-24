namespace Hr.Api.HiringNewEmployees;

public class HiringRequestsController : ControllerBase
{
    [HttpPost("/departments/IT/hiring-requests")]
    public async Task<ActionResult> HireAnEmployee(
        [FromBody] EmployeeHiringRequestModel request
        )
    {
        return Ok(new EmployeeHiringRequestResult(request.Name));
    }
}

public record EmployeeHiringRequestModel(string Name);

public record EmployeeHiringRequestResult(string Name);