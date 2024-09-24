using Hr.Api.Controllers;

namespace Hr.Tests;
public class HiringRequestsTests
{
    //name is required. Cannot be null, cannot be empty.Has to be at least 5 characters, and no more than 200
    [Theory]
    [InlineData("Robert")]
    [InlineData("Rober")]
    [InlineData("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX")]
    public void GoodNames(string name)
    {
        var _ = EmployeeHiringRequest.CreateHiringRequest(name, Departments.SALES);

    }

    [Theory]
    [InlineData("bob")]
    [InlineData("bobs")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("ZXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX")]
    public void InvalidNames(string name)
    {

        Assert.Throws<ArgumentOutOfRangeException>(() =>
        EmployeeHiringRequest.CreateHiringRequest(name, Departments.SALES));

    }


}


public static class HiringRequestSamples
{
    public static EmployeeHiringRequest BobInIt()
    {
        return EmployeeHiringRequest.CreateHiringRequest("Robert", Departments.IT);
    }

    public static IEnumerable<EmployeeHiringRequest> SampleHiringRequests()
    {
        yield return BobInIt();
        yield return SueInSales;
        yield return EmployeeHiringRequest.CreateHiringRequest("Joseph Schmidt", Departments.SUPPORT);
        yield return EmployeeHiringRequest.CreateHiringRequest("Patricia Jones", Departments.CEO);
        yield return EmployeeHiringRequest.CreateHiringRequest("Sandy F", Departments.IT);

    }
    public static EmployeeHiringRequest SueInSales = EmployeeHiringRequest.CreateHiringRequest("Susan", Departments.SALES);
}