using Microsoft.EntityFrameworkCore;
using Provider.Data;
using Provider.Data.Models;

namespace Provider.Services;

public static class ConfigQueries
{
    public static Task<GroupModel?> FindConfigsAsync(this AppDbContext dbContext, string groupName, params string[] configQueries)
    {
        return dbContext.Groups
            .Include(group => group.Configs.Where(config => configQueries.Any(query => config.Key.MatchesLQuery(query))))
            .FirstOrDefaultAsync(x => x.Name == groupName);
    }
}