using FluentValidation.TestHelper;
using Hr.Api.HiringNewEmployees;
namespace Hr.Tests.Employees;
public class EmployeeHiringRequestionValidationTests
{
    [Theory]
    [Trait("Category", "Unit")]
    [InlineData("Bo")]
    [InlineData(null)]
    [InlineData("1234")]
    [InlineData("ZXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX")]
    public void InvalidHiringRequests(string name)
    {

        var validator = new EmployeeHiringRequestValidator();

        var modelToValidate = new EmployeeHiringRequestModel { Name = name };

        var result = validator.TestValidate(modelToValidate);
        result.ShouldHaveValidationErrorFor(m => m.Name).WithErrorMessage("Invalid Name");
    }
}
