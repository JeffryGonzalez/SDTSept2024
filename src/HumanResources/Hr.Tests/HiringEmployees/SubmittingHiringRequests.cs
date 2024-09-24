
using Alba;
using Hr.Api.HiringNewEmployees;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Time.Testing;
using NSubstitute;

namespace Hr.Tests.HiringEmployees;
public class SubmittingHiringRequests
{
    [Theory]
    [Trait("Category", "System")]
    [Trait("Feature", "SomeFeatureName")]
    [Trait("Bug", "83989389")]
    [InlineData("Bob Smith")]
    [InlineData("Jill Jones")]
    public async Task SubmittingAHiringRequestForIt(string name)
    {
        var dateOfHire = new DateTimeOffset(1969, 4, 20, 23, 59, 00, TimeSpan.FromHours(-4));
        var stubbedIdGenerator = Substitute.For<IGenerateSlugIdsForEmployees>();
        stubbedIdGenerator.GenerateIdForAsync(name).Returns(name.ToUpper());
        var fakeClock = new FakeTimeProvider(dateOfHire);
        var host = await AlbaHost.For<Program>(config =>
        {   // When you want to replace a service with another one.
            config.ConfigureTestServices(services =>
            {
                services.AddSingleton<TimeProvider>((sp) => fakeClock);
                services.AddSingleton<IGenerateSlugIdsForEmployees>(sp => stubbedIdGenerator);
            });
        });

        var hiringRequest = new EmployeeHiringRequestModel(name);

        var expectedResponse = new EmployeeHiringRequestResult(name.ToUpper(), name, "IT", 182000M, dateOfHire);
        var response = await host.Scenario(api =>
         {
             api.Post.Json(hiringRequest).ToUrl("/departments/IT/hiring-requests");
         });

        var returnedBody = await response.ReadAsJsonAsync<EmployeeHiringRequestResult>();

        Assert.NotNull(returnedBody);
        Assert.Equal(expectedResponse, returnedBody);


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