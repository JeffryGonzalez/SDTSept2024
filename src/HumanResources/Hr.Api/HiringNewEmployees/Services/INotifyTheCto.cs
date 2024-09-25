using CioNotificationApiTypes;

namespace Hr.Api.HiringNewEmployees.Services;
public interface INotifyTheCto
{
    Task<NewItHiringNotificationResponse> NotifyCioOfNewItHireAsync(NewItHiringNotificationRequest request);
}