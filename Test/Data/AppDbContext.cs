using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Test.Data.Models;

namespace Test.Data;

public class AppDbContext : DbContext
{
    public required DbSet<EnvironmentModel> Environments { get; init; }
    public required DbSet<ConfigModel> Configs { get; init; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureEnvironmentModel(modelBuilder);
        ConfigureConfigModel(modelBuilder);
    }

    private void ConfigureConfigModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConfigModel>().HasKey(config => new { config.Key, config.EnvironmentId });
        modelBuilder.Entity<ConfigModel>().HasIndex(config => config.Key).HasMethod("gist");
        modelBuilder.Entity<ConfigModel>()
            .HasOne<EnvironmentModel>()
            .WithMany(env => env.Configs)
            .HasForeignKey(config => config.EnvironmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private void ConfigureEnvironmentModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EnvironmentModel>().HasKey(env => env.Id);
        modelBuilder.Entity<EnvironmentModel>().HasIndex(env => env.Name).IsUnique();
        modelBuilder.Entity<EnvironmentModel>()
            .HasOne(env => env.Parent)
            .WithMany()
            .IsRequired(false);
    }
}