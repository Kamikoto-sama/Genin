namespace Provider.Dto.Groups;

public class GroupCreateDto
{
    public required string Name { get; init; }
    public int? ParentId { get; init; }
}