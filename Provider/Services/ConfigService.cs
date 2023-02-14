using Provider.Data;
using Provider.Data.Models;
using Provider.Dto;

namespace Provider.Services;

public class ConfigService
{
    private readonly AppDbContext dbContext;

    public ConfigService(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task AddAsync(string envName, IEnumerable<AddConfigDto> configDtos)
    {
        var env = await dbContext.FindEnvAsync(envName);
        if (env == null)
            return;

        var configs = configDtos.Select(x => new ConfigModel
        {
            Key = x.Key.Replace("/", "."),
            Value = x.Value
        });

        env.Configs.AddRange(configs);
        await dbContext.SaveChangesAsync();
    }

    public async Task<ConfigDto[]> GetAsync(string envName, string[] keys)
    {
        var configs = new Dictionary<string, (ConfigModel config, string envName)>();
        keys = keys.Select(x => x.Replace("/", ".") + ".*").ToArray();
        do
        {
            var env = await dbContext.FindConfigsAsync(envName, keys);
            if (env == null)
                break;

            foreach (var config in env.Configs)
                configs.TryAdd(config.Key, (config, env.Name));

            if (env.Parent == null)
                break;

            envName = env.Parent.Name;
        } while (configs.Count < keys.Length);

        return configs.Select(x => new ConfigDto
        {
            Key = x.Key.Replace(".", "/"),
            Value = x.Value.config.Value,
            EnvironmentName = x.Value.envName
        }).ToArray();
    }

    public async Task UpdateValueAsync(string envName, AddConfigDto configDto)
    {
        var query = configDto.Key.Replace("/", ".");
        var env = await dbContext.FindConfigsAsync(envName, query);
        if (env == null || env.Configs.Count != 1)
            return;

        var config = env.Configs.Single();
        config.Value = configDto.Value;
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(string envName, string[] keys)
    {
        keys = keys.Select(x => x.Replace("/", ".")).ToArray();
        var env = await dbContext.FindConfigsAsync(envName, keys);
        if (env == null)
            return;

        env.Configs.Clear();
        await dbContext.SaveChangesAsync();
    }
}