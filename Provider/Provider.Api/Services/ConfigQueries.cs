using Microsoft.EntityFrameworkCore;
using Provider.Api.Data;
using Provider.Api.Data.Models;

namespace Provider.Api.Services;

public static class ConfigQueries
{
    public static Task<ZoneModel?> LoadConfigsByPrefix(this AppDbContext dbContext, string zoneName, IEnumerable<string> keyPrefixes)
    {
        var keys = keyPrefixes.Select(x => x + ".*").ToArray();
        return dbContext.LoadConfigs(zoneName, keys);
    }

    public static Task<ZoneModel?> LoadConfigs(this AppDbContext dbContext, string zoneName, params string[] keys)
    {
        return dbContext.Zones
            .Include(zone => zone.Configs.Where(config => keys.Any(query => config.Key.MatchesLQuery(query))))
            .FirstOrDefaultAsync(zone => zone.Name == zoneName);
    }

    public static Task<ZoneModel?> LoadAllConfigs(this AppDbContext dbContext, string zoneName)
    {
        return dbContext.Zones
            .Include(zone => zone.Configs)
            .FirstOrDefaultAsync(zone => zone.Name == zoneName);
    }
}