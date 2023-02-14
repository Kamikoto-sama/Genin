namespace Test.Data.Models;

public record EnvironmentModel
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public virtual EnvironmentModel? Parent { get; init; }
    public virtual List<ConfigModel> Configs { get; init; } = new();
}