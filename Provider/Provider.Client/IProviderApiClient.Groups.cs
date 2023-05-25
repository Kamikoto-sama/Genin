using Provider.Dto.Groups;
using Refit;

namespace Provider.Client;

public partial interface IProviderApiClient
{
    [Post("/api/groups/create/{name}")]
    Task<IApiResponse> CreateGroup(string name, string? parentName);

    [Get("/api/groups/{groupName}")]
    Task<IApiResponse<GroupDto>> GetGroup(string groupName);

    [Get("/api/groups/all")]
    Task<IApiResponse<GroupDto[]>> GetAllGroups();

    [Post("/api/groups/{groupName}/updateName")]
    Task<IApiResponse> UpdateGroupName(string groupName, string newName);

    [Post("/api/groups/{groupName}/setParent")]
    Task<IApiResponse> SetGroupParent(string groupName, string parentName);

    [Post("/api/groups/{groupName}/removeParent")]
    Task<IApiResponse> RemoveGroupParent(string groupName);

    [Post("/api/groups/{groupName}/delete")]
    Task<IApiResponse> DeleteGroup(string groupName);
}