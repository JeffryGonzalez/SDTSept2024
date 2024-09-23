namespace Hr.Api;

public class EmployeeHiringService
{

    public Employee Hire(EmployeeHiringRequest request)
    {
        // Treat exceptions as pretty stinky code smells.
        if (string.IsNullOrEmpty(request.Name))
        {
            throw new ArgumentOutOfRangeException(nameof(request.Name));
        }

        var salary = request.Department == Departments.IT ? 180000M : 42000M;
        var id = request.Department == Departments.IT ? "I" : "S";
        return new Employee(id + Guid.NewGuid().ToString(), request.Name, request.Department, salary);
    }
}



public record Employee(string Id, string Name, Departments Department, decimal Salary, DateTimeOffset HireDate);

