using Microsoft.EntityFrameworkCore;

namespace Provider.Api.Data.Models;

public record ConfigModel
{
    public required LTree Key { get; init; }
    public int ZoneId { get; init; }
    public required string Value { get; set; }
}