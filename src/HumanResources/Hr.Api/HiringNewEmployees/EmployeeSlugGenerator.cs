
namespace Hr.Api.HiringNewEmployees;

public class EmployeeSlugGenerator(ICheckForSlugUniqueness uniquenessChecker) : IGenerateSlugIdsForEmployees
{
    public async Task<string> GenerateIdForItAsync(string name)
    {
        // is the name not null, is it at least 5 letters, and no more than 200
        // Johnny Marr -> IMARR-JOHNNY-B
        var spaceAt = name.IndexOf(' ');
        var firstName = name[..spaceAt];
        var lastName = name[(spaceAt + 1)..];
        firstName = firstName.Replace(' ', '-');
        lastName = lastName.Replace(' ', '-');
        var proposedSlug = $"I{lastName.ToUpper()}-{firstName.ToUpper()}";
        if (await uniquenessChecker.IsUniqueIdAsync(proposedSlug))
        {
            return proposedSlug;
        }
        else
        {
            var letters = "abcdefghijklmnopqrstuvwxyz".ToUpper().Select(c => c).ToList();
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

    public async Task<string> GenerateIdForNonItAsync(string name)
    {
        return "SJONES-JILL";
    }
}

public interface ICheckForSlugUniqueness
{
    Task<bool> IsUniqueIdAsync(string proposedSlug);
}