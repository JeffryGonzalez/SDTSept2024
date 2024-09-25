namespace Hr.Api.HiringNewEmployees.Models;

public record EmployeeHiringRequestResult(string Id, string Name, string Department, decimal Salary, DateTimeOffset HireDate);
public record EmployeeHiringRequestResponseModel
{
    public required HiringRequestPersonalInformation PersonalInformation { get; init; }

    public required DateTimeOffset ApplicationDate { get; init; }
    public required string Status { get; init; }

    public Dictionary<string, string> Links { get; init; } = new();
}

public record HiringRequestPersonalInformation
{
    public required string Name { get; init; }
    public required string DepartmentAppliedTo { get; init; }
}


