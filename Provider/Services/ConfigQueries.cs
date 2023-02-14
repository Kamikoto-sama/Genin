using Microsoft.EntityFrameworkCore;
using Provider.Data;
using Provider.Data.Models;

namespace Provider.Services;

public static class ConfigQueries
{
    public static Task<EnvironmentModel?> FindConfigsAsync(this AppDbContext dbContext, string envName, params string[] configQueries)
    {
        return dbContext.Environments
            .Include(env => env.Configs.Where(config => configQueries.Any(query => config.Key.MatchesLQuery(query))))
            .FirstOrDefaultAsync(x => x.Name == envName);
    }
}