namespace Hr.Api.HiringNewEmployees;

public class HiringRequestsController(TimeProvider clock, IGenerateSlugIdsForEmployees employeeIdGenerator) : ControllerBase
{
    [HttpPost("/departments/IT/hiring-requests")]
    public async Task<ActionResult> HireAnEmployee(
        [FromBody] EmployeeHiringRequestModel request
        )
    {
        string id = await employeeIdGenerator.GenerateIdForAsync(request.Name);
        return Ok(new EmployeeHiringRequestResult(id, request.Name, "IT", 182000M, clock.GetUtcNow()));
    }
}

public record EmployeeHiringRequestModel(string Name);

public record EmployeeHiringRequestResult(string Id, string Name, string Department, decimal Salary, DateTimeOffset HireDate);

public interface IGenerateSlugIdsForEmployees
{
    Task<string> GenerateIdForAsync(string name);  // Bob Smith - ISMITH-BOB
}