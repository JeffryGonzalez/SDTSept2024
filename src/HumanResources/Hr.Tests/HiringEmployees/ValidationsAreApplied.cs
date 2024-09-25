using Alba;
using Hr.Api.Controllers;
using Ht.Tests.HiringEmployees;

namespace Hr.Tests.HiringEmployees;
[Collection("SharedSystemTestFixture")]
public class ValidationsAreApplied
{
    private IAlbaHost _host = null!;
    public ValidationsAreApplied(SystemTestFixture fixture)
    {
        _host = fixture.Host;
    }

    [Fact]
    [Trait("Category", "System")]
    public async Task ValidatesEmployeeHiringRequests()
    {

        var hiringRequest = new EmployeeHiringRequestModel { Name = null! };


        var response = await _host.Scenario(api =>
        {
            api.Post.Json(hiringRequest).ToUrl("/departments/IT/hiring-requests");
            api.StatusCodeShouldBe(400);
        });

    }
}
