using FluentValidation;
using Hr.Api.HiringNewEmployees.Models;
using HtTemplate.Configuration;
using Marten;

var builder = WebApplication.CreateBuilder(args);

builder.AddCustomFeatureManagement();

builder.Services.AddCustomServices();
builder.Services.AddCustomOasGeneration();

builder.Services.AddControllers();

//builder.Services.AddSingleton<IGenerateEmployeeIds, StandardIdGenerator>();
//builder.Services.AddSingleton<EmployeeHiringService>();
//builder.Services.AddScoped<IGenerateSlugIdsForEmployees, EmployeeSlugGenerator>();
builder.Services.AddValidatorsFromAssemblyContaining<EmployeeHiringRequestValidator>(); // this is werid, but I'll show you why they do it this way tomorrow.
var connectionString = builder.Configuration.GetConnectionString("hr") ?? throw new Exception("No database connection string");
builder.Services.AddMarten(options =>
{
    options.Connection(connectionString);
}).UseLightweightSessions();

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

