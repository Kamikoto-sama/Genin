using Common;
using Common.Results;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Provider.Data;
using Provider.Dto.Configs;
using Provider.Mappings;

namespace Provider.Services;

public class ConfigService
{
    private readonly AppDbContext dbContext;

    public ConfigService(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result> AddAsync(string groupName, IEnumerable<ConfigAddDto> configDtos)
    {
        var configs = configDtos.Select(ConfigMapper.ToConfigModel).ToArray();

        var group = await dbContext.LoadConfigsByPrefix(groupName, configs.Select(x => x.Key.ToString()));
        if (group == null)
            return ConfigError.GroupNotFound();
        if (group.Configs.Count > 0)
            return ConfigError.KeysConflict(group.Configs.Select(x => x.Key));

        group.Configs.AddRange(configs);
        await dbContext.SaveChangesAsync();

        return Result.Ok();
    }

    public async Task<Result<ConfigDto[]>> GetAsync(string groupName, string[] keys)
    {
        var configs = new Dictionary<string, ConfigDto>();
        do
        {
            var group = await dbContext.LoadConfigsByPrefix(groupName, keys);
            if (group == null)
                return ConfigError.GroupNotFound();

            foreach (var config in group.Configs)
                configs.TryAdd(config.Key, config.ToConfigDto(group));

            if (group.Parent == null)
                break;

            groupName = group.Parent.Name;
        } while (configs.Count < keys.Length);

        return configs.Values.ToArray();
    }

    public async Task<Result> UpdateValueAsync(string groupName, ConfigUpdateDto dto)
    {
        var keys = dto.Key.Replace("/", ".");
        var group = await dbContext.LoadConfigs(groupName, keys);
        if (group == null)
            return ConfigError.GroupNotFound();

        var config = group.Configs.Single();
        config.Value = dto.Value;
        await dbContext.SaveChangesAsync();

        return Result.Ok();
    }

    public async Task<Result> DeleteAsync(string groupName, string[] keys)
    {
        keys = keys.Select(x => x.Replace("/", ".")).ToArray();
        var group = await dbContext.LoadConfigs(groupName, keys);
        if (group == null)
            return ConfigError.GroupNotFound();

        group.Configs.Clear();
        await dbContext.SaveChangesAsync();

        return Result.Ok();
    }
}

public class ConfigError
{
    public enum Code
    {
        GroupNotFound = 1,
        KeysConflict
    }

    public static Result GroupNotFound() => ResultHelper.InvalidOperation(Code.GroupNotFound);

    public static Result KeysConflict(IEnumerable<LTree> storedKeys) =>
        ResultHelper.InvalidOperation(Code.KeysConflict, $"Existing keys: {storedKeys.ToStringJoin()}");
}