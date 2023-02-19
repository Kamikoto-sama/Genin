namespace Provider.Dto.Configs;

public record AddConfigDto
{
    public required string Key { get; init; }
    public required string Value { get; init; }
}