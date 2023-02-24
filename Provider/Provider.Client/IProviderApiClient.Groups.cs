using Provider.Dto.Groups;
using Refit;

namespace Provider.Client;

public partial interface IProviderApiClient
{
    [Post("/api/group/{groupName}/create")]
    public Task<ApiResponse<int>> CreateGroup(string groupName);

    [Get("/api/group/{groupName}")]
    public Task<ApiResponse<GroupDto>> GetGroup(string groupName);

    [Post("/api/group/{groupName}/updateName")]
    public Task<IApiResponse> UpdateGroupName(string groupName, string newName);

    [Post("/api/group/{groupName}/setParent")]
    public Task<IApiResponse> SetGroupParent(string groupName, int parentId);

    [Post("/api/group/{groupName}/removeParent")]
    public Task<IApiResponse> RemoveGroupParent(string groupName);

    [Post("/api/group/{groupName}/delete")]
    public Task<IApiResponse> DeleteGroup(string groupName);
}