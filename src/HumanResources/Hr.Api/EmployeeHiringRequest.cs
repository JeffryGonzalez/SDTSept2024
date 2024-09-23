namespace Hr.Api;

//// department is required, and it has to have the values of "IT", "HR", "CEO", "SALES", "SUPPORT"

public enum Departments { IT, HR, CEO, SALES, SUPPORT };
public record EmployeeHiringRequest

{
    public string Name { get; private set; } = string.Empty;
    public Departments Department { get; private set; }
    // A "factory" method.
    public static EmployeeHiringRequest CreateHiringRequest(string name, Departments department)
    {


        if (IsCorrectLength(name))
        {
            throw new ArgumentOutOfRangeException();
        }
        return new EmployeeHiringRequest { Name = name, Department = department };
    }

    // "Don't type 'private', always refactor to it.
    private static bool IsCorrectLength(string name)
    {
        return string.IsNullOrEmpty(name) || name.Length < 5 || name.Length > 200;
    }
}

