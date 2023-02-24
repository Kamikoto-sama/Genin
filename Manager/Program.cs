using Serilog;
using Serilog.Events;

namespace Manager;

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
        appBuilder.Services.AddControllers();
    }

    private static void ConfigureApp(WebApplication app)
    {
        app.UseSerilogRequestLogging();

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.MapControllers();
        app.MapFallbackToFile("index.html");
    }
}