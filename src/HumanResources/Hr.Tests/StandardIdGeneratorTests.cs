using Hr.Api.Controllers;

namespace Hr.Tests;
public class StandardIdGeneratorTests
{
    [Fact]
    public void GeneratesIdsForPeopleThatAreInIt()
    {
        IGenerateEmployeeIds generator = new StandardIdGenerator();

        var result = generator.GetIdFor(Departments.IT);

        Assert.StartsWith("I", result);
    }


    [Fact]
    public void GeneratesIdsForPeopleThatAreNotIt()
    {
        IGenerateEmployeeIds generator = new StandardIdGenerator();

        var result = generator.GetIdFor(Departments.SALES); // make this a theory or multiple tests, whatever.
        var firstLetter = result[..1];
        var rest = result[1..];

        Assert.StartsWith(firstLetter, result);
        Guid.Parse(rest); // Throws if it is not a GUID
    }
}
