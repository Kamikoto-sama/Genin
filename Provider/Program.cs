using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Provider.Data;
using Provider.Services;

namespace Provider;

internal class Program
{
    private static async Task Main()
    {
        var appBuilder = WebApplication.CreateBuilder();

        ConfigureHost(appBuilder);
        ConfigureServices(appBuilder);

        var app = appBuilder.Build();
        ConfigureApp(app);

        await app.RunAsync();
    }

    private static void ConfigureHost(WebApplicationBuilder appBuilder)
    {
        appBuilder.Configuration.AddJsonFile("appsettings.private.json", true);
    }

    private static void ConfigureServices(WebApplicationBuilder appBuilder)
    {
        var services = appBuilder.Services;

        var connectionString = appBuilder.Configuration.GetConnectionString("postgres");
        services.AddDbContext<AppDbContext>(builder => builder.UseNpgsql(connectionString));

        services.AddScoped<GroupService>();
        services.AddScoped<ConfigService>();

        services.AddControllers();
        services.AddSwaggerGen();
        services
            .AddValidatorsFromAssemblyContaining<Program>()
            .AddFluentValidationAutoValidation(config => config.DisableDataAnnotationsValidation = true)
            .AddFluentValidationRulesToSwagger();
    }

    private static void ConfigureApp(WebApplication app)
    {
        app.UseHttpsRedirection();

        app.UseSwagger();
        app.UseSwaggerUI();

        if (app.Environment.IsDevelopment())
            app.MapGet("/", context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });

        app.MapControllers();
    }
}