using Hr.Api;

namespace Hr.Tests;
public class EmployeeHiringServiceTests
{

    [Fact]
    public void HiringAnItEmployee()
    {
        // Given
        var service = new EmployeeHiringService();
        var candidate = HiringRequestSamples.BobInIt();

        var expectedEmployee = new Employee(
            "I9b4e5a5a-3975-4396-8da3-2bd6a85e25ec",
            candidate.Name, candidate.Department,
            180_000M,
            new DateTimeOffset(2024, 9, 23, 3, 14, 00, TimeSpan.FromHours(-4))
            );

        // when
        var employee = service.Hire(candidate);

        // Then
        Assert.Equal(expectedEmployee, employee);
        //Assert.Equal(180000M, employee.Salary);
        //Assert.Equal(candidate.Department, employee.Department);
        //Assert.Equal(candidate.Name, employee.Name);
        //Assert.StartsWith("I", employee.Id);

    }

    [Fact]
    public void HiringANonItEmployee()
    {
        var service = new EmployeeHiringService();
        var candidate = HiringRequestSamples.SueInSales;

        // when
        var employee = service.Hire(candidate);

        // Then
        Assert.Equal(42000M, employee.Salary);
        Assert.Equal(candidate.Department, employee.Department);
        Assert.Equal(candidate.Name, employee.Name);
        Assert.StartsWith("S", employee.Id);
    }
}

/* Todo:
 * 
If you are are in the IT department:
Your employee ID starts with I, then a unique identifier.
Your starting salary is $180,000.

For *all* other departments:

Your employee ID starts with "S", then a unique identifier.
Your starting salary is $42,000.

*/