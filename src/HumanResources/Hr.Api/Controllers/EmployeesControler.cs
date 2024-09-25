namespace Hr.Api.Controllers;

public class EmployeesControler(EmployeeHiringService service) : ControllerBase
{

    //[HttpPost("/employees")]
    //public async Task<ActionResult> AddEmployeeAsync(
    //    [FromBody]
    //    EmployeeHiringRequestModel request)
    //{
    //    // To future generations that see this code, this is intentionally demonstrating horrible code.
    //    try
    //    {

    //        try
    //        {
    //            var department = Enum.Parse<Departments>(request.Department);
    //            var employeeToHire = EmployeeHiringRequest.CreateHiringRequest(request.Name, department);
    //            var response = service.Hire(employeeToHire);
    //            //service.Hire()

    //            return Ok(response);

    //        }
    //        catch (ArgumentException)
    //        {

    //            return BadRequest("That department doesn't exist");
    //        }


    //    }
    //    catch (ArgumentOutOfRangeException)
    //    {

    //        return BadRequest("Nice try, sucker.");
    //    }
    //}
}


public record EmployeeHiringRequestModel
{
    public string Name { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
}