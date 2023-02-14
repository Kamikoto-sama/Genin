using Microsoft.EntityFrameworkCore;

namespace Test.Data.Models;

public record ConfigModel
{
    public required LTree Key { get; init; }
    public int EnvironmentId { get; init; }
    public required string Value { get; init; }
}