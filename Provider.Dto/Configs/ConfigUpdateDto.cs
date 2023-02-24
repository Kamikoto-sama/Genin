namespace Provider.Dto.Configs;

public record ConfigUpdateDto
{
    public required string Key { get; init; }
    public required string Value { get; init; }
}