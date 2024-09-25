using Hr.Api.HiringNewEmployees.Services;
using NSubstitute;

namespace Hr.Tests.Employees;
public class SlugGeneratorTests
{

    [Theory]
    [Trait("Category", "Unit")]
    [InlineData("Bob Smith", "IT", "ismith-bob")] // First Bob Smith in IT
    [InlineData("Jill Jones", "CEO", "sjones-jill")] // First Ceo
    [InlineData("Johnny Marr", "IT", "imarr-johnny-b")] // Second Johnny Marr is INT
    [InlineData("Jeff Gonzalez", "IT", "igonzalez-jeff-k")]
    [InlineData("Joel Van Der Kuil", "IT", "ivan-der-kuil-joel-z")] // Lots of these in IT
    [InlineData("George Jones", "SALES", "sjones-george")]
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

        Assert.StartsWith("icaruso-eric", result);
        // read the rest of it and see if it is a GUID, like in that other test we did.
        Assert.Equal(49, result.Length);
        // there is always going to be "untestable" things, and that's ok.


    }
}
