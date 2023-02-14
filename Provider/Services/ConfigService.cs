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

    public async Task AddAsync(string groupName, IEnumerable<AddConfigDto> configDtos)
    {
        var group = await dbContext.FindGroupAsync(groupName);
        if (group == null)
            return;

        var configs = configDtos.Select(x => new ConfigModel
        {
            Key = x.Key.Replace("/", "."),
            Value = x.Value
        });

        group.Configs.AddRange(configs);
        await dbContext.SaveChangesAsync();
    }

    public async Task<ConfigDto[]> GetAsync(string groupName, string[] keys)
    {
        var configs = new Dictionary<string, (ConfigModel config, string groupName)>();
        keys = keys.Select(x => x.Replace("/", ".") + ".*").ToArray();
        do
        {
            var group = await dbContext.FindConfigsAsync(groupName, keys);
            if (group == null)
                break;

            foreach (var config in group.Configs)
                configs.TryAdd(config.Key, (config, group.Name));

            if (group.Parent == null)
                break;

            groupName = group.Parent.Name;
        } while (configs.Count < keys.Length);

        return configs.Select(x => new ConfigDto
        {
            Key = x.Key.Replace(".", "/"),
            Value = x.Value.config.Value,
            GroupName = x.Value.groupName
        }).ToArray();
    }

    public async Task UpdateValueAsync(string groupName, AddConfigDto configDto)
    {
        var query = configDto.Key.Replace("/", ".");
        var group = await dbContext.FindConfigsAsync(groupName, query);
        if (group == null || group.Configs.Count != 1)
            return;

        var config = group.Configs.Single();
        config.Value = configDto.Value;
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(string groupName, string[] keys)
    {
        keys = keys.Select(x => x.Replace("/", ".")).ToArray();
        var group = await dbContext.FindConfigsAsync(groupName, keys);
        if (group == null)
            return;

        group.Configs.Clear();
        await dbContext.SaveChangesAsync();
    }
}