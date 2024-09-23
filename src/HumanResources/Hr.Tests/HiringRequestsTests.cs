
using Hr.Api;

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
        var _ = EmployeeHiringRequest.CreateHiringRequest(name, "IT");

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
        EmployeeHiringRequest.CreateHiringRequest(name, "tacos"));

    }
}
