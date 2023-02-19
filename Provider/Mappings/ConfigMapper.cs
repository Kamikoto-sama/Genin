using Provider.Data.Models;
using Provider.Dto.Configs;

namespace Provider.Mappings;

public static class ConfigMapper
{
    public static ConfigDto ToConfigDto(this ConfigModel configModel, GroupModel groupModel)
    {
        return new ConfigDto
        {
            Key = configModel.Key.ToString().Replace(".", "/"),
            Value = configModel.Value,
            GroupName = groupModel.Name,
            GroupId = groupModel.Id
        };
    }

    public static ConfigModel ToConfigModel(this AddConfigDto configDto)
    {
        return new ConfigModel
        {
            Key = configDto.Key.Replace("/", "."),
            Value = configDto.Value
        };
    }
}