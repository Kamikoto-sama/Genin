using Microsoft.EntityFrameworkCore;
using Provider.Data;
using Provider.Data.Models;

namespace Provider.Services;

public class GroupService
{
    private readonly AppDbContext dbContext;

    public GroupService(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<int> CreateAsync(string groupName, int? parentId)
    {
        GroupModel? parent = null;
        if (parentId != null)
            parent = await dbContext.Groups.FindAsync(parentId);

        var group = new GroupModel
        {
            Name = groupName,
            Parent = parent
        };
        var entry = await dbContext.Groups.AddAsync(group);
        await dbContext.SaveChangesAsync();

        return entry.Entity.Id;
    }

    public async Task SetParentAsync(string groupName, int? parentId)
    {
        GroupModel? parent = null;
        if (parentId != null)
            parent = await dbContext.Groups.FindAsync(parentId);

        var group = await dbContext.FindGroupAsync(groupName);
        group!.Parent = parent;
        await dbContext.SaveChangesAsync();
    }

    public async Task<GroupModel?> GetAsync(string groupName) => await dbContext.FindGroupAsync(groupName);

    public async Task UpdateNameAsync(string groupName, string newName)
    {
        var group = await dbContext.FindGroupAsync(groupName);
        group!.Name = newName;
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(string groupName)
    {
        var group = await dbContext.FindGroupAsync(groupName);
        dbContext.Groups.Remove(group!);
        await dbContext.SaveChangesAsync();
    }
}