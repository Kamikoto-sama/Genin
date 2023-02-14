using Microsoft.EntityFrameworkCore;

namespace Provider.Data.Models;

public record ConfigModel
{
    public required LTree Key { get; init; }
    public int EnvironmentId { get; init; }
    public required string Value { get; set; }
}