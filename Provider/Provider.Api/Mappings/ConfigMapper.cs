using Provider.Api.Data.Models;
using Provider.Dto.Configs;

namespace Provider.Api.Mappings;

public static class ConfigMapper
{
    public static ConfigDto ToConfigDto(this ConfigModel configModel, ZoneModel zoneModel)
    {
        return new ConfigDto
        {
            Key = configModel.Key.ToString().Replace(".", "/"),
            Value = configModel.Value,
            ZoneName = zoneModel.Name,
        };
    }

    public static ConfigModel ToConfigModel(this ConfigAddDto dto)
    {
        return new ConfigModel
        {
            Key = dto.Key.Replace("/", "."),
            Value = dto.Value
        };
    }
}