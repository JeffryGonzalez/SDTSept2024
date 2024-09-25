using FluentValidation;
using Hr.Api.HiringNewEmployees;
using HtTemplate.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddCustomFeatureManagement();

builder.Services.AddCustomServices();
builder.Services.AddCustomOasGeneration();

builder.Services.AddControllers();

//builder.Services.AddSingleton<IGenerateEmployeeIds, StandardIdGenerator>();
//builder.Services.AddSingleton<EmployeeHiringService>();
//builder.Services.AddScoped<IGenerateSlugIdsForEmployees, EmployeeSlugGenerator>();
builder.Services.AddValidatorsFromAssemblyContaining<EmployeeHiringRequestValidator>(); // this is werid, but I'll show you why they do it this way tomorrow.


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run(); // 

// Top Level Statements - 

public partial class Program { }

