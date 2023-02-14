namespace Provider.Dto;

public class GroupDto
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public string? ParentName { get; init; }
}