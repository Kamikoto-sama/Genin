namespace Provider.Dto.Zones;

public class ZoneDto
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public ZoneDto? Parent { get; init; }
}