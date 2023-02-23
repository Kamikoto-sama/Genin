using Microsoft.EntityFrameworkCore;
using Provider.Data.Models;

namespace Provider.Data;

public class AppDbContext : DbContext
{
    public required DbSet<GroupModel> Groups { get; init; }
    public required DbSet<ConfigModel> Configs { get; init; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureGroupModel(modelBuilder);
        ConfigureConfigModel(modelBuilder);
    }

    private void ConfigureConfigModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConfigModel>().HasKey(config => new { config.Key, config.GroupId });
        modelBuilder.Entity<ConfigModel>()
            .HasOne<GroupModel>()
            .WithMany(group => group.Configs)
            .HasForeignKey(config => config.GroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private void ConfigureGroupModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GroupModel>().HasKey(group => group.Id);
        modelBuilder.Entity<GroupModel>().HasIndex(group => group.Name).IsUnique();
        modelBuilder.Entity<GroupModel>()
            .HasOne(group => group.Parent)
            .WithMany()
            .IsRequired(false);
    }
}