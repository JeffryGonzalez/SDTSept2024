namespace Hr.Api;

public class EmployeeHiringService(TimeProvider clock, IGenerateEmployeeIds idCreator)
{

    public Employee Hire(EmployeeHiringRequest request)
    {
        // Treat exceptions as pretty stinky code smells.
        if (string.IsNullOrEmpty(request.Name))
        {
            throw new ArgumentOutOfRangeException(nameof(request.Name));
        }

        var salary = request.Department == Departments.IT ? 180000M : 42000M;
        // when in doubt, WTCYWYH
        string id = idCreator.GetIdFor(request.Department);
        return new Employee(id, request.Name, request.Department, salary, clock.GetUtcNow());
    }
}

public interface IGenerateEmployeeIds
{
    string GetIdFor(Departments department);
}

public record Employee(string Id, string Name, Departments Department, decimal Salary, DateTimeOffset HireDate);

