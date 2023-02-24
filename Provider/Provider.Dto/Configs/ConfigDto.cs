namespace Provider.Dto.Configs;

public record ConfigDto
{
    public required string Key { get; init; }
    public required string Value { get; init; }
    public required string GroupName { get; init; }
    public required int GroupId { get; init; }
}