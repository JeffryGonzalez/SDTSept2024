namespace Hr.Api.HiringNewEmployees.Services;

public class EmployeeSlugGenerator(ICheckForSlugUniqueness uniquenessChecker) : IGenerateSlugIdsForEmployees
{
    public async Task<string> GenerateIdForItAsync(string name)
    {

        return await GenerateSlug(name, "i");
    }
    public async Task<string> GenerateIdForNonItAsync(string name)
    {
        return await GenerateSlug(name, "S");
    }

    private async Task<string> GenerateSlug(string name, string prefix)
    {
        var spaceAt = name.IndexOf(' ');
        var firstName = name[..spaceAt];
        var lastName = name[(spaceAt + 1)..];
        firstName = firstName.Replace(' ', '-');
        lastName = lastName.Replace(' ', '-');
        var proposedSlug = $"{prefix}{lastName}-{firstName}".ToLower();
        if (await uniquenessChecker.IsUniqueIdAsync(proposedSlug))
        {
            return proposedSlug;
        }
        else
        {
            var letters = "abcdefghijklmnopqrstuvwxyz".Select(c => c).ToList();
            foreach (var letter in letters)
            {
                var attempt = proposedSlug + "-" + letter;
                if (await uniquenessChecker.IsUniqueIdAsync(attempt))
                {
                    return attempt;
                }
            }
            return proposedSlug + "-" + Guid.NewGuid();

        }
    }

}

public interface ICheckForSlugUniqueness
{
    Task<bool> IsUniqueIdAsync(string proposedSlug);
}