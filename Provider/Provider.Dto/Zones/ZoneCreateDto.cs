namespace Provider.Dto.Zones;

public class ZoneCreateDto
{
    public required string Name { get; init; }
    public int? ParentId { get; init; }
}