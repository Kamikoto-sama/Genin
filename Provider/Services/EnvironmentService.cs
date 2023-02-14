using Microsoft.EntityFrameworkCore;
using Provider.Data;
using Provider.Data.Models;

namespace Provider.Services;

public class EnvironmentService
{
    private readonly AppDbContext dbContext;

    public EnvironmentService(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<int> CreateAsync(string envName, int? parentId)
    {
        EnvironmentModel? parent = null;
        if (parentId != null)
            parent = await dbContext.Environments.FindAsync(parentId);

        var env = new EnvironmentModel
        {
            Name = envName,
            Parent = parent
        };
        var entry = await dbContext.Environments.AddAsync(env);
        await dbContext.SaveChangesAsync();

        return entry.Entity.Id;
    }

    public async Task SetParentAsync(string envName, int? parentId)
    {
        EnvironmentModel? parent = null;
        if (parentId != null)
            parent = await dbContext.Environments.FindAsync(parentId);

        var env = await dbContext.FindEnvAsync(envName);
        env!.Parent = parent;
        await dbContext.SaveChangesAsync();
    }

    public async Task<EnvironmentModel?> GetAsync(string envName) => await dbContext.FindEnvAsync(envName);

    public async Task UpdateNameAsync(string envName, string newName)
    {
        var env = await dbContext.FindEnvAsync(envName);
        env!.Name = newName;
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(string envName)
    {
        var env = await dbContext.FindEnvAsync(envName);
        dbContext.Environments.Remove(env!);
        await dbContext.SaveChangesAsync();
    }
}