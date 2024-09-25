using FluentValidation;

namespace Hr.Api.HiringNewEmployees;

public record EmployeeHiringRequestModel
{

    public string Name { get; set; } = string.Empty;
};
public class EmployeeHiringRequestValidator : AbstractValidator<EmployeeHiringRequestModel>
{
    public EmployeeHiringRequestValidator()
    {
        var message = "Invalid Name";
        //RuleFor(e => e.Name).NotEmpty().MinimumLength(5).MaximumLength(200).WithMessage("Invalid Name");
        RuleFor(e => e.Name).NotEmpty().WithMessage(message);
        RuleFor(e => e.Name).MinimumLength(5).WithMessage(message).MaximumLength(200).WithMessage(message);

    }
}

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

/*{
    "id": "GUID",
    "personalInformation": {
        "name": "Bob Smith",
        "departmentAppliedTo": "{department}"
    }, 
    "applicationDate": "DTO",
    "status": "Hired",
    "links": [        
        "self": "/departments/{department}/hiring-requests/GUID"
        "employee": "/employees/ismith-bob",
        "employee:role": "/departments/it",
        "hiring-requests:submitter": "/employees/sjones-sue"

    ]

}*/
