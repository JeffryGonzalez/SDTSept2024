using System.Net.Http.Json;

namespace Hr.Tests.HiringEmployees;
public class EmployeeHiringIntegrationTest
{
    [Fact]
    [Trait("Category", "SystemIntegration")]
    public async Task CanHireAnEmployee()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:1337"); // Where you have your API running in a staging environment.

        var response = await client.PostAsJsonAsync("/departments/it/hiring-requests", new { Name = "Samuel" });

        response.EnsureSuccessStatusCode();
        // deserialize the rsponse, all that.

    }
}
