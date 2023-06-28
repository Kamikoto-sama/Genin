using Microsoft.EntityFrameworkCore;
using Provider.Api.Data.Models;

namespace Provider.Api.Data;

public class AppDbContext : DbContext
{
    public required DbSet<ZoneModel> Zones { get; init; }
    public required DbSet<ConfigModel> Configs { get; init; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureZoneModel(modelBuilder);
        ConfigureConfigModel(modelBuilder);
    }

    private void ConfigureConfigModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConfigModel>().HasKey(config => new { config.Key, config.ZoneId });
        modelBuilder.Entity<ConfigModel>()
            .HasOne<ZoneModel>()
            .WithMany(zone => zone.Configs)
            .HasForeignKey(config => config.ZoneId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private void ConfigureZoneModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ZoneModel>().HasKey(zone => zone.Id);
        modelBuilder.Entity<ZoneModel>().HasIndex(zone => zone.Name).IsUnique();
        modelBuilder.Entity<ZoneModel>()
            .HasOne(zone => zone.Parent)
            .WithMany()
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
    }
}