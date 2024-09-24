
using Alba;
using Hr.Api.HiringNewEmployees;

namespace Hr.Tests.HiringEmployees;
public class SubmittingHiringRequests
{
    [Fact]
    public async Task Submitting()
    {
        var host = await AlbaHost.For<Program>();

        var hiringRequest = new EmployeeHiringRequestModel("Bob Smith");
        var expectedResponse = new EmployeeHiringRequestResult("Bob Smith");
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