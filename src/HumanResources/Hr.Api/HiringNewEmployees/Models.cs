﻿using FluentValidation;

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
