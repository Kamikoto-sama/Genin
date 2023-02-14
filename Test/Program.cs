using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Test.Data;
using Test.Data.Models;
using Utils;

namespace Test;

internal static class Program
{
    private static async Task Main()
    {
        var provider = ConfigureServices();
        using var scope = provider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        // await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();
        await Run(dbContext);
    }

    private static async Task Run(AppDbContext dbContext)
    {
        // await CreateEnvAsync(dbContext);
        await AddConfig(dbContext);
        // await QueryConfigs(dbContext);
    }

    private static async Task QueryConfigs(AppDbContext dbContext)
    {
        var query = new[] { "a.b.c", "x.y.z" };
        var env = await dbContext.GetAsync("a", query);
        Console.WriteLine(env.Configs.ToStringJoin("\n"));
    }

    private static async Task<EnvironmentModel> GetAsync(this AppDbContext dbContext, string envName, string[] queries)
    {
        return await dbContext.Environments
            .Include(env => env.Configs.Where(config => queries.Any(query => config.Key.MatchesLQuery(query))))
            .FirstAsync(x => x.Name == envName);
    }

    private static async Task AddConfig(AppDbContext dbContext)
    {
        var env = await dbContext.Environments.FirstAsync(x => x.Name == "a");
        var configs = new[]
        {
            new ConfigModel { Key = "x.x.x", Value = "5" },
        };
        env.Configs.AddRange(configs);
        await dbContext.SaveChangesAsync();
    }

    private static async Task CreateEnvAsync(AppDbContext dbContext)
    {
        var env = new EnvironmentModel { Name = "a" };
        var entry = dbContext.Environments.Add(env);
        await dbContext.SaveChangesAsync();
        Console.WriteLine(entry.Entity.Id);
    }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        const string connectionString = "Username=postgres;Password=postgres;Database=genin;Host=localhost;Port=5432;";
        services.AddDbContext<AppDbContext>(builder => builder.UseNpgsql(connectionString));

        return services.BuildServiceProvider();
    }
}