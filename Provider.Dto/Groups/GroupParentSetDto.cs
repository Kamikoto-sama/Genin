namespace Provider.Dto.Groups;

public class GroupParentSetDto
{
    public required string Name { get; init; }
    public int? ParentId { get; init; }
}