
using Alba;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Time.Testing;

namespace Ht.Tests.HiringEmployees;

public class SystemTestFixture : IAsyncLifetime
{
    public IAlbaHost Host = null!;
    public async Task InitializeAsync()
    {
        var dateOfHire = new DateTimeOffset(1969, 4, 20, 23, 59, 00, TimeSpan.FromHours(-4));


        var fakeClock = new FakeTimeProvider(dateOfHire);
        Host = await AlbaHost.For<Program>(config =>
        {
            config.ConfigureTestServices(services =>
            {
                services.AddSingleton<TimeProvider>((sp) => fakeClock);


            });
        });
    }
    public async Task DisposeAsync()
    {
        await Host.DisposeAsync();
    }

}
[CollectionDefinition("SharedSystemTestFixture")]
public class SharedSystemTestFixture : ICollectionFixture<SystemTestFixture> { }