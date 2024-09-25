var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.MapPost("/new-hire-notifications", async (ItNotificationRequest request) =>
{
    await Task.Delay(3000);
    return Results.Ok(new ItNotificationResponse { NotificationDeliveryReceipt = Guid.NewGuid() });
}).WithOpenApi();

app.Run();

internal record ItNotificationRequest
{
    public string Name { get; set; } = string.Empty;
    public DateTimeOffset WhenHired { get; set; }
}

internal record ItNotificationResponse
{
    public Guid NotificationDeliveryReceipt { get; set; }
}
