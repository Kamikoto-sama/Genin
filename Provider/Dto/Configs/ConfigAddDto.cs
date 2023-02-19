namespace Provider.Dto.Configs;

public record ConfigAddDto
{
    public required string Key { get; init; }
    public required string Value { get; init; }
}