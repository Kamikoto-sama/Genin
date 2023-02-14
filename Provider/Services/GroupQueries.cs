using Microsoft.EntityFrameworkCore;
using Provider.Data;
using Provider.Data.Models;

namespace Provider.Services;

public static class GroupQueries
{
    public static Task<GroupModel?> FindGroupAsync(this AppDbContext dbContext, string groupName)
    {
        return dbContext.Groups.FirstOrDefaultAsync(group => group.Name == groupName);
    }
}