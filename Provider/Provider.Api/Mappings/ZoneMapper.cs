using Provider.Api.Data.Models;
using Provider.Dto.Zones;

namespace Provider.Api.Mappings;

public static class ZoneMapper
{
    public static ZoneModel ToZoneModel(string zoneName, ZoneModel? parentId)
    {
        return new ZoneModel
        {
            Name = zoneName,
            Parent = parentId
        };
    }

    public static ZoneDto ToZoneDto(ZoneModel zoneModel)
    {
        var parent = zoneModel.Parent;
        return new ZoneDto
        {
            Id = zoneModel.Id,
            Name = zoneModel.Name,
            Parent = parent == null
                ? null
                : new ZoneDto
                {
                    Id = parent.Id,
                    Name = parent.Name
                }
        };
    }

    public static ZoneDto[] ToZoneDto(ZoneModel[] zoneModels) => zoneModels.Select(ToZoneDto).ToArray();
}