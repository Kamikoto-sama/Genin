namespace Provider.Dto.Groups;

public class GroupDto
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public GroupDto? Parent { get; init; }
}