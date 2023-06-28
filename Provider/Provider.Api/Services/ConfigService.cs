using Common;
using Common.Results;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Provider.Api.Data;
using Provider.Api.Mappings;
using Provider.Dto.Configs;

namespace Provider.Api.Services;

public class ConfigService
{
    private readonly AppDbContext dbContext;

    public ConfigService(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result> AddAsync(string zoneName, IEnumerable<ConfigAddDto> configDtos)
    {
        var configs = configDtos.Select(ConfigMapper.ToConfigModel).ToArray();

        var zone = await dbContext.LoadConfigsByPrefix(zoneName, configs.Select(x => x.Key.ToString()));
        if (zone == null)
            return ConfigError.ZoneNotFound();
        if (zone.Configs.Count > 0)
            return ConfigError.KeysConflict(zone.Configs.Select(x => x.Key));

        zone.Configs.AddRange(configs);
        await dbContext.SaveChangesAsync();

        return Result.Ok();
    }

    public async Task<Result<ConfigDto[]>> GetAsync(string zoneName, string[] keys)
    {
        var configs = new Dictionary<string, ConfigDto>();
        do
        {
            var zone = await dbContext.LoadConfigsByPrefix(zoneName, keys);
            if (zone == null)
                return ConfigError.ZoneNotFound();

            foreach (var config in zone.Configs)
                configs.TryAdd(config.Key, config.ToConfigDto(zone));

            if (zone.Parent == null)
                break;

            zoneName = zone.Parent.Name;
        } while (configs.Count < keys.Length);

        return configs.Values.ToArray();
    }

    public async Task<Result> UpdateValueAsync(string zoneName, ConfigUpdateDto dto)
    {
        var keys = dto.Key.Replace("/", ".");
        var zone = await dbContext.LoadConfigs(zoneName, keys);
        if (zone == null)
            return ConfigError.ZoneNotFound();

        var config = zone.Configs.Single();
        config.Value = dto.Value;
        await dbContext.SaveChangesAsync();

        return Result.Ok();
    }

    public async Task<Result> DeleteAsync(string zoneName, string[] keys)
    {
        keys = keys.Select(x => x.Replace("/", ".")).ToArray();
        var zone = await dbContext.LoadConfigs(zoneName, keys);
        if (zone == null)
            return ConfigError.ZoneNotFound();

        zone.Configs.Clear();
        await dbContext.SaveChangesAsync();

        return Result.Ok();
    }
}

public class ConfigError
{
    public enum Code
    {
        ZoneNotFound = 1,
        KeysConflict
    }

    public static Result ZoneNotFound() => ResultHelper.InvalidOperation(Code.ZoneNotFound);

    public static Result KeysConflict(IEnumerable<LTree> storedKeys) =>
        ResultHelper.InvalidOperation(Code.KeysConflict, $"Existing keys: {storedKeys.ToStringJoin()}");
}