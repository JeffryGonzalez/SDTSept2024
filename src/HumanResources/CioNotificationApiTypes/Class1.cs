namespace CioNotificationApiTypes;


public class NewItHiringNotificationRequest
{
    public string Name { get; set; }
    public DateTimeOffset WhenHired { get; set; }
}


public class NewItHiringNotificationResponse
{
    public string NotificationDeliveryReceipt { get; set; } = string.Empty;
}