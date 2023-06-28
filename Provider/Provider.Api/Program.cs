using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Provider.Api.Data;
using Provider.Api.Services;
using Serilog;
using Serilog.Events;

namespace Provider.Api;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var appBuilder = WebApplication.CreateBuilder(args);

        ConfigureHost(appBuilder);
        ConfigureServices(appBuilder);

        var app = appBuilder.Build();

        ConfigureApp(app);

        await app.RunAsync();
    }

    private static void ConfigureHost(WebApplicationBuilder appBuilder)
    {
        ConfigureAppConfiguration(appBuilder);
        ConfigureLogging(appBuilder);
    }

    private static void ConfigureLogging(WebApplicationBuilder appBuilder)
    {
        const string template = "[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}";
        appBuilder.Host.UseSerilog((_, configuration) => configuration
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
            .WriteTo.Console(outputTemplate: template));
    }

    private static void ConfigureAppConfiguration(WebApplicationBuilder appBuilder)
    {
        appBuilder.Configuration.AddJsonFile("appsettings.private.json", true);
    }

    private static void ConfigureServices(WebApplicationBuilder appBuilder)
    {
        appBuilder.WebHost.UseDefaultServiceProvider(options => options.ValidateOnBuild = true);
        var services = appBuilder.Services;

        var connectionString = appBuilder.Configuration.GetConnectionString("postgres");
        services.AddDbContext<AppDbContext>(builder => builder.UseNpgsql(connectionString));

        services.AddScoped<ZoneService>();
        services.AddScoped<ConfigService>();

        services.AddControllers();
        services.AddSwaggerGen();
        services.AddValidatorsFromAssemblyContaining<Program>();
    }

    private static void ConfigureApp(WebApplication app)
    {
        app.UseSerilogRequestLogging();

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