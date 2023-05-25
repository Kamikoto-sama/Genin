using Common.Results;
using FluentResults;
using Microsoft.EntityFrameworkCore;
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

    public async Task<Result> Create(string groupName, string? parentName)
    {
        var group = await dbContext.FindGroupAsync(groupName);
        if (group != null)
            return GroupError.AlreadyExists();

        var parentResult = await GetParent(parentName);
        if (parentResult.IsFailed)
            return parentResult.ToResult();

        group = GroupMapper.ToGroupModel(groupName, parentResult.Value);
        await dbContext.Groups.AddAsync(group);
        await dbContext.SaveChangesAsync();

        return Result.Ok();
    }

    public async Task<Result> SetParent(string groupName, string? parentName)
    {
        var parentResult = await GetParent(parentName);
        if (parentResult.IsFailed)
            return parentResult.ToResult();

        var group = await dbContext.FindGroupAsync(groupName);
        if (group == null)
            return GroupError.NotFound();

        group.Parent = parentResult.Value;
        await dbContext.SaveChangesAsync();

        return Result.Ok();
    }

    private async Task<Result<GroupModel?>> GetParent(string? parentName)
    {
        GroupModel? parent = null;
        if (parentName == null)
            return parent;

        parent = await dbContext.Groups.SingleOrDefaultAsync(g => g.Name == parentName);
        if (parent == null)
            return GroupError.ParentNotFound();
        return parent;
    }

    public async Task<Result<GroupModel>> Get(string groupName)
    {
        var group = await dbContext.FindGroupAsync(groupName);
        return group == null ? GroupError.NotFound() : group;
    }

    public async Task<Result> UpdateName(string groupName, string newName)
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

    public async Task<Result> Delete(string groupName)
    {
        var group = await dbContext.FindGroupAsync(groupName);
        if (group == null)
            return GroupError.NotFound();

        dbContext.Groups.Remove(group);
        await dbContext.SaveChangesAsync();

        return Result.Ok();
    }

    public async Task<Result<GroupModel[]>> GetAll()
    {
        var groups = await dbContext.Groups.ToArrayAsync();
        return groups;
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