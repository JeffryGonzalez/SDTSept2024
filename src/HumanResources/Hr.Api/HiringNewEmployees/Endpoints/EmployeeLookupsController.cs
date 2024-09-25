using Hr.Api.HiringNewEmployees.Entities;
using Marten;

namespace Hr.Api.HiringNewEmployees.Endpoints;

public class EmployeeLookupsController(ILookupEmployees lookup) : ControllerBase
{
    [HttpGet("/departments/{department}/employees/{employeeId}")]
    public async Task<ActionResult> GetEmployeeById(string department, string employeeId)
    {
        var entity = await lookup.GetEmployeeByIdAsync(employeeId);

        if (entity is null)
        {
            return NotFound();
        }
        else
        {
            return Ok(new { Name = entity.PersonalInformation.Name });
        }
    }
}

public interface ILookupEmployees
{
    Task<EmployeeHiringRequestEntity?> GetEmployeeByIdAsync(string employeeId);
}

public class EmployeeLookup(IDocumentSession session) : ILookupEmployees
{
    public async Task<EmployeeHiringRequestEntity?> GetEmployeeByIdAsync(string employeeId)
    {
        var entity = await session.Query<EmployeeHiringRequestEntity>()
           .Where(e => e.EmployeeId == employeeId).SingleOrDefaultAsync();
        return entity;
    }
}