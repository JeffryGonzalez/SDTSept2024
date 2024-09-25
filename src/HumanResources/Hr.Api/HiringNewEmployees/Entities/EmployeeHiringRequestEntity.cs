using Hr.Api.HiringNewEmployees.Models;

namespace Hr.Api.HiringNewEmployees.Entities;

// TODO 1 - Show the entity
public record EmployeeHiringRequestEntity
{
    public required Guid Id { get; set; }

    public required HiringRequestPersonalInformation PersonalInformation { get; init; }

    public required DateTimeOffset ApplicationDate { get; init; }
    public required string Status { get; init; }
    public required string SubmittedBy { get; init; }
    public string? EmployeeId { get; set; }

    public string? CioReceipt { get; set; }
    public Dictionary<string, string> Links { get; init; } = new();
}


