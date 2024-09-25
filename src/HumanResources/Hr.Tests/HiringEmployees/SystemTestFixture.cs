
using Alba;
using Alba.Security;
using CioNotificationApiTypes;
using Hr.Api.HiringNewEmployees.Services;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Time.Testing;
using NSubstitute;
using Testcontainers.PostgreSql;
using WireMock.Server;

namespace Ht.Tests.HiringEmployees;

public class SystemTestFixture : IAsyncLifetime
{
    public IAlbaHost Host = null!;
    private PostgreSqlContainer _container = new PostgreSqlBuilder()
        .WithImage("postgres:16.2-bullseye")
        .Build();
    public WireMockServer MockApiServer = null!;
    public async Task InitializeAsync()
    {

        MockApiServer = WireMockServer.Start();
        var dateOfHire = new DateTimeOffset(1969, 4, 20, 23, 59, 00, TimeSpan.FromHours(-4));
        var stubbedUser = new AuthenticationStub("Barbara");
        var fakeCioNotification = Substitute.For<INotifyTheCto>();
        fakeCioNotification.NotifyCioOfNewItHireAsync(Arg.Any<NewItHiringNotificationRequest>())

            .Returns(new NewItHiringNotificationResponse { NotificationDeliveryReceipt = "Looks Good" });
        var fakeClock = new FakeTimeProvider(dateOfHire);
        await _container.StartAsync();
        Host = await AlbaHost.For<Program>(config =>
        {
            config.UseSetting("ConnectionStrings:hr", _container.GetConnectionString());
            config.UseSetting("cioApiUrl", MockApiServer.Url);
            config.ConfigureTestServices(services =>
            {

                services.AddSingleton<TimeProvider>((sp) => fakeClock);
                //  services.AddScoped<INotifyTheCto>(sp => fakeCioNotification);

            });
        }, stubbedUser);
    }
    public async Task DisposeAsync()
    {
        await Host.DisposeAsync();
        await _container.StopAsync();
        MockApiServer.Stop();
        MockApiServer.Dispose();
    }

}
[CollectionDefinition("SharedSystemTestFixture")]
public class SharedSystemTestFixture : ICollectionFixture<SystemTestFixture> { }