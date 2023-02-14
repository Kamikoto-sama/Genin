namespace Provider.Data.Models;

public record EnvironmentModel
{
    public int Id { get; init; }
    public required string Name { get; set; }
    public virtual EnvironmentModel? Parent { get; set; }
    public virtual List<ConfigModel> Configs { get; init; } = new();
}