
using Alba;
using Hr.Api.HiringNewEmployees;
using Hr.Api.HiringNewEmployees.Services;
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
        stubbedIdGenerator.GenerateIdForItAsync(name).Returns(name.ToUpper());
        var fakeClock = new FakeTimeProvider(dateOfHire);
        var host = await AlbaHost.For<Program>(config =>
        {   // When you want to replace a service with another one.
            config.ConfigureTestServices(services =>
            {
                services.AddSingleton<TimeProvider>((sp) => fakeClock);
                var fakeUniqueChecker = Substitute.For<ICheckForSlugUniqueness>();
                fakeUniqueChecker.IsUniqueIdAsync(Arg.Any<string>()).Returns(true);
                services.AddScoped<ICheckForSlugUniqueness>((sp) => fakeUniqueChecker);
            });
        });

        var hiringRequest = new EmployeeHiringRequestModel { Name = name };

        var expectedResponse = new EmployeeHiringRequestResponseModel
        {

            ApplicationDate = dateOfHire,
            Status = "Hired",
            PersonalInformation = new HiringRequestPersonalInformation { DepartmentAppliedTo = "IT", Name = name }
        };
        var response = await host.Scenario(api =>
         {
             api.Post.Json(hiringRequest).ToUrl("/departments/IT/hiring-requests");
         });

        var returnedBody = await response.ReadAsJsonAsync<EmployeeHiringRequestResponseModel>();

        Assert.NotNull(returnedBody);
        // Assert.Equal(expectedResponse, returnedBody);
        var newResource = returnedBody.Links["self"];

        var lookupResponse = await host.Scenario(api =>
        {
            api.Get.Url(newResource);
        });


        var lookupBody = await response.ReadAsJsonAsync<EmployeeHiringRequestResponseModel>();
        Assert.NotNull(lookupResponse);
        Assert.Equal(lookupBody, returnedBody);

    }

    [Fact]
    [Trait("Category", "System")]
    public async Task ValidatesEmployeeHiringRequests()
    {
        var dateOfHire = new DateTimeOffset(1969, 4, 20, 23, 59, 00, TimeSpan.FromHours(-4));
        var stubbedIdGenerator = Substitute.For<IGenerateSlugIdsForEmployees>();
        stubbedIdGenerator.GenerateIdForItAsync("x").Returns("x");


        var fakeClock = new FakeTimeProvider(dateOfHire);
        var host = await AlbaHost.For<Program>(config =>
        {   // When you want to replace a service with another one.
            config.ConfigureTestServices(services =>
            {
                services.AddSingleton<TimeProvider>((sp) => fakeClock);
                var fakeUniqueChecker = Substitute.For<ICheckForSlugUniqueness>();
                fakeUniqueChecker.IsUniqueIdAsync(Arg.Any<string>()).Returns(true);
                services.AddScoped<ICheckForSlugUniqueness>((sp) => fakeUniqueChecker);
            });
        });

        var hiringRequest = new EmployeeHiringRequestModel { Name = null };


        var response = await host.Scenario(api =>
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