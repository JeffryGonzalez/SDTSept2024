
using Alba;
using Hr.Api.HiringNewEmployees.Models;
using Ht.Tests.HiringEmployees;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Hr.Tests.HiringEmployees;
[Collection("SharedSystemTestFixture")]
public class SubmittingHiringRequests
{
    private IAlbaHost _host = null!;

    public SubmittingHiringRequests(SystemTestFixture fixture)
    {
        _host = fixture.Host;
        fixture.MockApiServer.Given(
            Request.Create().WithPath("/new-hire-notifications")
            .UsingPost()
           ).RespondWith(
            Response.Create()
            .WithBodyAsJson(new CioNotificationApiTypes.NewItHiringNotificationResponse { NotificationDeliveryReceipt = "Tacos ARe Good" }));
    }


    [Theory]
    [Trait("Category", "System")]
    [Trait("Feature", "SomeFeatureName")]
    [Trait("Bug", "83989389")]
    [InlineData("Bob Smith")]
    [InlineData("Jill Jones")]
    public async Task SubmittingAHiringRequestForIt(string name)
    {
        // If this is sumbitted for IT, you are going to be an employee, right away.
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


        var employeeResponse = await _host.Scenario(api =>
        {
            api.Get.Url(lookupBody.Links["departments:employee"]);
        });

        var employeeBody = await employeeResponse.ReadAsJsonAsync<DummyModel>();

        Assert.NotNull(employeeBody);
        Assert.Equal(name, employeeBody.Name);
    }



}

public class DummyModel
{
    public string Name { get; set; }
}

/* System Test
We are going to start with testing this from the perspective of the "user" of this API.
The system tests should be the accurate description of the capabilities of your system.
You submit a hiring request for a department, we return you an employee.

Scenario:
    - Send a hiring request to /departments/IT/hiring-requests
    - You should get back....
*/