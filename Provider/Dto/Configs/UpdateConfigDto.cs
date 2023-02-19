namespace Provider.Dto.Configs;

public record UpdateConfigDto
{
    public required string Key { get; init; }
    public required string Value { get; init; }
}