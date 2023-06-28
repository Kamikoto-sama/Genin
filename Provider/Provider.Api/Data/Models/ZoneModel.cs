namespace Provider.Api.Data.Models;

public record ZoneModel
{
    public int Id { get; init; }
    public required string Name { get; set; }
    public virtual ZoneModel? Parent { get; set; }
    public virtual List<ConfigModel> Configs { get; init; } = new();
}