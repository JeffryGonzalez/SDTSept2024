namespace Hr.Api.HiringNewEmployees.Services;
public interface IGenerateSlugIdsForEmployees
{
    Task<string> GenerateIdForItAsync(string name);  // Bob Smith - ISMITH-BOB
    Task<string> GenerateIdForNonItAsync(string name);
}