namespace Provider.Api.Data.Models;

public record GroupModel
{
    public int Id { get; init; }
    public required string Name { get; set; }
    public virtual GroupModel? Parent { get; set; }
    public virtual List<ConfigModel> Configs { get; init; } = new();
}