using Hr.Api.Controllers;
using Microsoft.Extensions.Time.Testing;
using NSubstitute;

namespace Hr.Tests;
public class EmployeeHiringServiceTests
{

    [Theory]
    [MemberData(nameof(ItCandidates))]
    [Trait("Category", "Unit")]
    public void HiringAnItEmployee(EmployeeHiringRequest candidate)
    {
        // Given
        var currentTime = new DateTimeOffset(2024, 9, 23, 3, 14, 00, TimeSpan.FromHours(-4));
        var testTime = new FakeTimeProvider(currentTime);

        var fakeIdGenerator = Substitute.For<IGenerateEmployeeIds>();
        fakeIdGenerator.GetIdFor(Departments.IT).Returns("some-unique-it-identifier");
        var service = new EmployeeHiringService(testTime, fakeIdGenerator);


        var expectedEmployee = new Employee(
            "some-unique-it-identifier",
            candidate.Name, candidate.Department,
            180_000M,
           currentTime
            );

        // when
        var employee = service.Hire(candidate); // SUT

        // Then
        Assert.Equal(expectedEmployee, employee);


    }

    [Theory]
    [MemberData(nameof(NonIteCandidates))]
    [Trait("Category", "Unit")]
    public void HiringNonItEmployees(EmployeeHiringRequest candidate)
    {
        // Given
        // 4-20-69 - Jeff's Birthday.
        var currentTime = new DateTimeOffset(1969, 4, 20, 23, 59, 00, TimeSpan.FromHours(-4));
        var testTime = new FakeTimeProvider(currentTime);

        var fakeIdGenerator = Substitute.For<IGenerateEmployeeIds>();
        fakeIdGenerator.GetIdFor(Arg.Any<Departments>()).Returns("some-non-it-identifier");
        var service = new EmployeeHiringService(testTime, fakeIdGenerator);


        var expectedEmployee = new Employee(
            "some-non-it-identifier",
            candidate.Name, candidate.Department,
            42_000M,
           currentTime
            );

        // when
        var employee = service.Hire(candidate); // SUT

        // Then
        Assert.Equal(expectedEmployee, employee);


    }



    public static IEnumerable<object[]> ItCandidates()
    {
        var itFolks = HiringRequestSamples.SampleHiringRequests().Where(h => h.Department == Departments.IT);
        var response = new List<object[]>();
        foreach (var f in itFolks)
        {
            response.Add(new object[] { f });
        }
        return response;
    }
    public static IEnumerable<object[]> NonIteCandidates()
    {
        var itFolks = HiringRequestSamples.SampleHiringRequests().Where(h => h.Department != Departments.IT);
        var response = new List<object[]>();
        foreach (var f in itFolks)
        {
            response.Add(new object[] { f });
        }
        return response;
    }

}


/*
 * 
If you are are in the IT department:
Your employee ID starts with I, then a unique identifier.
Your starting salary is $180,000.

For *all* other departments:

Your employee ID starts with "S", then a unique identifier.
Your starting salary is $42,000.

*/

public class TestingIdGenerator : IGenerateEmployeeIds
{
    public string GetIdFor(Departments department)
    {
        if (department == Departments.IT)
        {
            return "some-unique-it-identifier";

        }
        else
        {
            return null;
        }
    }
}