
using Alba;
using Alba.Security;
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

        var stubbedUser = new AuthenticationStub().WithName("Barbara").With("role", "manager"); // // sub claim
        var fakeClock = new FakeTimeProvider(dateOfHire);
        Host = await AlbaHost.For<Program>(config =>
        {
            config.ConfigureTestServices(services =>
            {
                //    var fakeEmployeeLookup = Substitute.For<EmployeeLookup>();
                //    var fakeEmployee = new EmployeeHiringRequestEntity
                //    {
                //        ApplicationDate = dateOfHire,
                //        Id = Guid.NewGuid(),
                //        EmployeeId = "Idon't know",
                //        PersonalInformation = new Hr.Api.HiringNewEmployees.Models.HiringRequestPersonalInformation { Name = "Some Name", DepartmentAppliedTo = "IT" },
                //        Status = "Unknown",
                //        SubmittedBy = "Barbara"

                //    };
                //    fakeEmployeeLookup.GetEmployeeByIdAsync(Arg.Any<string>()).Returns(
                //        fakeEmployee);
                services.AddSingleton<TimeProvider>((sp) => fakeClock);
                // services.AddScoped<ILookupEmployees>(sp => fakeEmployeeLookup);

            });
        }, stubbedUser);
    }
    public async Task DisposeAsync()
    {
        await Host.DisposeAsync();
    }

}
[CollectionDefinition("SharedSystemTestFixture")]
public class SharedSystemTestFixture : ICollectionFixture<SystemTestFixture> { }