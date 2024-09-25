
using Hr.Api.HiringNewEmployees.Entities;
using Marten;

namespace Hr.Api.HiringNewEmployees.Services;

public class EmployeeIdUniquenessChecker(IDocumentSession session) : ICheckForSlugUniqueness
{
    public async Task<bool> IsUniqueIdAsync(string proposedSlug)
    {
        var any = await
            session.Query<EmployeeHiringRequestEntity>().AnyAsync(e => e.EmployeeId == proposedSlug);
        return !any;
    }
}
