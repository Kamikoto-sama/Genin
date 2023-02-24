using Provider.Dto.Configs;
using Refit;

namespace Provider.Client;

public partial interface IProviderApiClient
{
    [Post("/api/{groupName}/configs/add")]
    public Task<IApiResponse> AddConfigs(string groupName, [Body] ConfigAddDto[] configs);

    [Post("/api/{groupName}/configs/updateValue")]
    public Task<IApiResponse> UpdateConfigValue(string groupName, [Body] ConfigUpdateDto[] configs);

    [Get("/api/{groupName}/configs")]
    public Task<ApiResponse<ConfigDto[]>> GetConfigs(string groupName, [Body] string[] keys);

    [Post("/api/{groupName}/configs/delete")]
    public Task<IApiResponse> DeleteConfigs(string groupName, [Body] string[] keys);
}