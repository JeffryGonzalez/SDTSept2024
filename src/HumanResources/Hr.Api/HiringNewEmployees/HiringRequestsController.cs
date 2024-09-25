using FluentValidation;

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

public record EmployeeHiringRequestModel
{


    public string Name { get; set; } = string.Empty;
};

public class EmployeeHiringRequestValidator : AbstractValidator<EmployeeHiringRequestModel>
{
    public EmployeeHiringRequestValidator()
    {
        var message = "Invalid Name";
        //RuleFor(e => e.Name).NotEmpty().MinimumLength(5).MaximumLength(200).WithMessage("Invalid Name");
        RuleFor(e => e.Name).NotEmpty().WithMessage(message);
        RuleFor(e => e.Name).MinimumLength(5).WithMessage(message).MaximumLength(200).WithMessage(message);

    }
}

public record EmployeeHiringRequestResult(string Id, string Name, string Department, decimal Salary, DateTimeOffset HireDate);

public interface IGenerateSlugIdsForEmployees
{
    Task<string> GenerateIdForItAsync(string name);  // Bob Smith - ISMITH-BOB
    Task<string> GenerateIdForNonItAsync(string name);
}