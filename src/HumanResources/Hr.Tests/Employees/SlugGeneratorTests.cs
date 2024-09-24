

using Hr.Api.HiringNewEmployees;
using NSubstitute;

namespace Hr.Tests.Employees;
public class SlugGeneratorTests
{

    [Theory]
    [Trait("Category", "Unit")]
    [InlineData("Bob Smith", "IT", "ISMITH-BOB")] // First Bob Smith in IT
    [InlineData("Jill Jones", "CEO", "SJONES-JILL")] // First Ceo
    [InlineData("Johnny Marr", "IT", "IMARR-JOHNNY-B")] // Second Johnny Marr is INT
    [InlineData("Jeff Gonzalez", "IT", "IGONZALEZ-JEFF-K")]
    [InlineData("Joel Van Der Kuil", "IT", "IVAN-DER-KUIL-JOEL-Z")] // Lots of these in IT
    //[InlineData("Eric Caruso", "IT", "ICARUSO-ERIC-82ca117f-7e69-4f6d-a28b-7383aac7f734")] // add a guid as a last resort
    public async Task GeneratingSlugs(string name, string department, string expected)
    {

        var fakeUniqueChecker = Substitute.For<ICheckForSlugUniqueness>();
        fakeUniqueChecker.IsUniqueIdAsync(expected).Returns(true);
        IGenerateSlugIdsForEmployees slugGenerator = new EmployeeSlugGenerator(fakeUniqueChecker);

        if (department == "IT")
        {
            var result = await slugGenerator.GenerateIdForItAsync(name);
            Assert.Equal(expected, result);
        }
        else
        {
            var result = await slugGenerator.GenerateIdForNonItAsync(name);
            Assert.Equal(expected, result);
        }
    }

    [Fact]
    public async Task LongNamesGetAGuidAddedToTheEnd()
    {
        //  //[InlineData("Eric Caruso", "IT", "ICARUSO-ERIC-82ca117f-7e69-4f6d-a28b-7383aac7f734")] 
        var fakeUniqueChecker = Substitute.For<ICheckForSlugUniqueness>();
        //fakeUniqueChecker.IsUniqueIdAsync(expected).Returns(true); // never finds a unique name
        IGenerateSlugIdsForEmployees slugGenerator = new EmployeeSlugGenerator(fakeUniqueChecker);

        var result = await slugGenerator.GenerateIdForItAsync("Eric Caruso");

        Assert.StartsWith("ICARUSO-ERIC", result);
        // read the rest of it and see if it is a GUID, like in that other test we did.
        Assert.Equal(49, result.Length);
        // there is always going to be "untestable" things, and that's ok.


    }
}
