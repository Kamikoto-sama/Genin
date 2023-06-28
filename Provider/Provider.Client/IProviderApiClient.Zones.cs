using Provider.Dto.Zones;
using Refit;

namespace Provider.Client;

public partial interface IProviderApiClient
{
    [Post("/api/zones/create/{name}")]
    Task<IApiResponse> CreateZone(string name, string? parentName);

    [Get("/api/zones/{zoneName}")]
    Task<IApiResponse<ZoneDto>> GetZone(string zoneName);

    [Get("/api/zones/all")]
    Task<IApiResponse<ZoneDto[]>> GetAllZones();

    [Post("/api/zones/{zoneName}/updateName")]
    Task<IApiResponse> UpdateZoneName(string zoneName, string newName);

    [Post("/api/zones/{zoneName}/setParent")]
    Task<IApiResponse> SetZoneParent(string zoneName, string parentName);

    [Post("/api/zones/{zoneName}/removeParent")]
    Task<IApiResponse> RemoveZoneParent(string zoneName);

    [Post("/api/zones/{zoneName}/delete")]
    Task<IApiResponse> DeleteZone(string zoneName);
}