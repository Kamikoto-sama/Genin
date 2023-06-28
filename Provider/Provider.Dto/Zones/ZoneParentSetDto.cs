namespace Provider.Dto.Zones;

public class ZoneParentSetDto
{
    public required string Name { get; init; }
    public int? ParentId { get; init; }
}