namespace Provider.Dto;

public class EnvCreateDto
{
    public required string Name { get; init; }
    public int? ParentId { get; init; }
}