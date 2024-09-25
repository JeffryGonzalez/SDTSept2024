using CioNotificationApiTypes;

namespace Hr.Api.HiringNewEmployees.Services;

public class CioNotificationHttpClient(HttpClient client) : INotifyTheCto
{
    public async Task<NewItHiringNotificationResponse> NotifyCioOfNewItHireAsync(NewItHiringNotificationRequest request)
    {
        var response = await client.PostAsJsonAsync("/new-hire-notifications", request);
        response.EnsureSuccessStatusCode();

        var entity = await response.Content.ReadFromJsonAsync<NewItHiringNotificationResponse>();
        if (entity is null)
        {
            throw new Exception("Nothing Returned");
        }
        return entity;

    }
}
