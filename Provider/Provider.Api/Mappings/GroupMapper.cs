using Provider.Api.Data.Models;
using Provider.Dto.Groups;

namespace Provider.Api.Mappings;

public static class GroupMapper
{
    public static GroupModel ToGroupModel(string groupName, GroupModel? parentId)
    {
        return new GroupModel
        {
            Name = groupName,
            Parent = parentId
        };
    }

    public static GroupDto ToGroupDto(GroupModel groupModel)
    {
        var parent = groupModel.Parent;
        return new GroupDto
        {
            Id = groupModel.Id,
            Name = groupModel.Name,
            Parent = parent == null
                ? null
                : new GroupDto
                {
                    Id = parent.Id,
                    Name = parent.Name
                }
        };
    }
}