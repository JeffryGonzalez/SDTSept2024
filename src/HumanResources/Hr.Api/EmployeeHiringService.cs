namespace Hr.Api;

public class EmployeeHiringService
{

    public Employee Hire(EmployeeHiringRequest request)
    {

        return new Employee("99", "99", "99", 5);
    }
}


public record EmployeeHiringRequest(string Name, string Department);

public record Employee(string Id, string Name, string Department, decimal Salary);

