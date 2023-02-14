using Microsoft.EntityFrameworkCore;
using Provider.Data;
using Provider.Data.Models;

namespace Provider.Services;

public static class EnvironmentQueries
{
    public static Task<EnvironmentModel?> FindEnvAsync(this AppDbContext dbContext, string envName)
    {
        return dbContext.Environments.FirstOrDefaultAsync(env => env.Name == envName);
    }
}