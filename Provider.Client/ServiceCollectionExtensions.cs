using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Provider.Client;

public static class ServiceCollectionExtensions
{
    public static void AddProviderApiClient(this IServiceCollection services, ProviderApiClientOptions options)
    {
        services.AddRefitClient<IProviderApiClient>()
            .ConfigureHttpClient(client => client.BaseAddress = new Uri(options.ApiUrl));
    }
}

public record ProviderApiClientOptions
{
    public required string ApiUrl { get; init; }
}