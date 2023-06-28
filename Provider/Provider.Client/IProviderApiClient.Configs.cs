using Provider.Dto.Configs;
using Refit;

namespace Provider.Client;

public partial interface IProviderApiClient
{
    [Post("/api/{zoneName}/configs/add")]
    public Task<IApiResponse> AddConfigs(string zoneName, [Body] ConfigAddDto[] configs);

    [Post("/api/{zoneName}/configs/updateValue")]
    public Task<IApiResponse> UpdateConfigValue(string zoneName, [Body] ConfigUpdateDto[] configs);

    [Get("/api/{zoneName}/configs")]
    public Task<ApiResponse<ConfigDto[]>> GetConfigs(string zoneName, [Body] string[] keys);

    [Post("/api/{zoneName}/configs/delete")]
    public Task<IApiResponse> DeleteConfigs(string zoneName, [Body] string[] keys);
}