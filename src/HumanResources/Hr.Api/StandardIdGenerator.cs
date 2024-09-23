namespace Hr.Api;

public class StandardIdGenerator : IGenerateEmployeeIds
{
    public string GetIdFor(Departments department)
    {
        return department == Departments.IT ? "I" + Guid.NewGuid().ToString() : "S" + Guid.NewGuid().ToString();
    }
}
