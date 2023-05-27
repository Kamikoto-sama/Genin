using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Provider.Client;

public static class ServiceCollectionExtensions
{
    public static void AddProviderApiClient(this IServiceCollection services, ProviderApiClientOptions options)
    {
        services.AddRefitClient<IProviderApiClient>()
            .ConfigureHttpClient(client => client.BaseAddress = new Uri(options.Url));
    }
}

public record ProviderApiClientOptions
{
    public required string Url { get; init; }
}