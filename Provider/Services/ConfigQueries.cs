using Microsoft.EntityFrameworkCore;
using Provider.Data;
using Provider.Data.Models;

namespace Provider.Services;

public static class ConfigQueries
{
    public static Task<GroupModel?> LoadConfigsByPrefix(this AppDbContext dbContext, string groupName, IEnumerable<string> keyPrefixes)
    {
        var keys = keyPrefixes.Select(x => x + ".*").ToArray();
        return dbContext.LoadConfigs(groupName, keys);
    }

    public static Task<GroupModel?> LoadConfigs(this AppDbContext dbContext, string groupName, params string[] keys)
    {
        return dbContext.Groups
            .Include(group => group.Configs.Where(config => keys.Any(query => config.Key.MatchesLQuery(query))))
            .FirstOrDefaultAsync(x => x.Name == groupName);
    }
}