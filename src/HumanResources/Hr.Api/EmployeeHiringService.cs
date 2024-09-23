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

        return new Employee("99", "99", "99", 5);
    }
}


public record EmployeeHiringRequest

{
    public string Name { get; private set; } = string.Empty;
    public string Department { get; private set; } = string.Empty;
    // A "factory" method.
    public static EmployeeHiringRequest CreateHiringRequest(string name, string department)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentOutOfRangeException();
        }
        return new EmployeeHiringRequest { Name = name, Department = department };
    }
}



public record Employee(string Id, string Name, string Department, decimal Salary);

