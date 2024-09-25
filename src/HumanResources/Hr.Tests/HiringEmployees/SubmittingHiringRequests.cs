
using Alba;
using Hr.Api.HiringNewEmployees.Models;
using Hr.Api.HiringNewEmployees.Services;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Time.Testing;
using NSubstitute;

namespace Hr.Tests.HiringEmployees;
public class SubmittingHiringRequests : IAsyncLifetime
{
    private IAlbaHost _host = null!;
    public async Task InitializeAsync()
    {
        var dateOfHire = new DateTimeOffset(1969, 4, 20, 23, 59, 00, TimeSpan.FromHours(-4));
        var stubbedIdGenerator = Substitute.For<IGenerateSlugIdsForEmployees>();

        var fakeClock = new FakeTimeProvider(dateOfHire);
        _host = await AlbaHost.For<Program>(config =>
        {
            config.ConfigureTestServices(services =>
            {
                services.AddSingleton<TimeProvider>((sp) => fakeClock);
                var fakeUniqueChecker = Substitute.For<ICheckForSlugUniqueness>();

            });
        });
    }
    public async Task DisposeAsync()
    {
        await _host.DisposeAsync();
    }


    [Theory]
    [Trait("Category", "System")]
    [Trait("Feature", "SomeFeatureName")]
    [Trait("Bug", "83989389")]
    [InlineData("Bob Smith")]
    [InlineData("Jill Jones")]
    public async Task SubmittingAHiringRequestForIt(string name)
    {
        var hiringRequest = new EmployeeHiringRequestModel { Name = name };
        var response = await _host.Scenario(api =>
         {
             api.Post.Json(hiringRequest).ToUrl("/departments/it/hiring-requests");
         });
        var returnedBody = await response.ReadAsJsonAsync<EmployeeHiringRequestResponseModel>();

        Assert.NotNull(returnedBody);

        var newResource = returnedBody.Links["self"];

        var lookupResponse = await _host.Scenario(api =>
        {
            api.Get.Url(newResource);
        });


        var lookupBody = await response.ReadAsJsonAsync<EmployeeHiringRequestResponseModel>();
        Assert.NotNull(lookupResponse);
        Assert.Equal(lookupBody.PersonalInformation, returnedBody.PersonalInformation);
        Assert.Equal(lookupBody.ApplicationDate, returnedBody.ApplicationDate);
        Assert.Equal(lookupBody.Status, returnedBody.Status);
        Assert.Equal(lookupBody.Links, returnedBody.Links); // Records don't do deep equality.

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


/* System Test
We are going to start with testing this from the perspective of the "user" of this API.
The system tests should be the accurate description of the capabilities of your system.
You submit a hiring request for a department, we return you an employee.

Scenario:
    - Send a hiring request to /departments/IT/hiring-requests
    - You should get back....
*/