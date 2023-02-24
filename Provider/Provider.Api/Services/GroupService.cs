using Common.Results;
using FluentResults;
using Provider.Api.Data;
using Provider.Api.Data.Models;
using Provider.Api.Mappings;

namespace Provider.Api.Services;

public class GroupService
{
    private readonly AppDbContext dbContext;

    public GroupService(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<int>> CreateAsync(string groupName, int? parentId)
    {
        var group = await dbContext.FindGroupAsync(groupName);
        if (group != null)
            return GroupError.AlreadyExists();

        var parentResult = await GetParentAsync(parentId);
        if (parentResult.IsFailed)
            return parentResult.ToResult();

        group = GroupMapper.ToGroupModel(groupName, parentResult.Value);
        var entry = await dbContext.Groups.AddAsync(group);
        await dbContext.SaveChangesAsync();

        return entry.Entity.Id;
    }

    public async Task<Result> SetParentAsync(string groupName, int? parentId)
    {
        var parentResult = await GetParentAsync(parentId);
        if (parentResult.IsFailed)
            return parentResult.ToResult();

        var group = await dbContext.FindGroupAsync(groupName);
        if (group == null)
            return GroupError.NotFound();

        group.Parent = parentResult.Value;
        await dbContext.SaveChangesAsync();

        return Result.Ok();
    }

    private async Task<Result<GroupModel?>> GetParentAsync(int? parentId)
    {
        GroupModel? parent = null;
        if (parentId == null)
            return parent;

        parent = await dbContext.Groups.FindAsync(parentId);
        if (parent == null)
            return GroupError.ParentNotFound();
        return parent;
    }

    public async Task<Result<GroupModel>> GetAsync(string groupName)
    {
        var group = await dbContext.FindGroupAsync(groupName);
        return group == null ? GroupError.NotFound() : group;
    }

    public async Task<Result> UpdateNameAsync(string groupName, string newName)
    {
        var group = await dbContext.FindGroupAsync(groupName);
        if (group == null)
            return GroupError.NotFound();

        if (await dbContext.FindGroupAsync(newName) != null)
            return GroupError.AlreadyExists();

        group.Name = newName;
        await dbContext.SaveChangesAsync();

        return Result.Ok();
    }

    public async Task<Result> DeleteAsync(string groupName)
    {
        var group = await dbContext.FindGroupAsync(groupName);
        if (group == null)
            return GroupError.NotFound();

        dbContext.Groups.Remove(group);
        await dbContext.SaveChangesAsync();

        return Result.Ok();
    }
}

public class GroupError
{
    public enum Code
    {
        GroupNotFound = 1,
        GroupAlreadyExists,
        ParentNotFound
    }

    public static Result NotFound() => ResultHelper.NotFound(Code.GroupNotFound);

    public static Result AlreadyExists() => ResultHelper.InvalidOperation(Code.GroupAlreadyExists);

    public static Result ParentNotFound() => ResultHelper.InvalidOperation(Code.ParentNotFound);
}