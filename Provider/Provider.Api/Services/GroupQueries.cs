using Microsoft.EntityFrameworkCore;
using Provider.Api.Data;
using Provider.Api.Data.Models;

namespace Provider.Api.Services;

public static class GroupQueries
{
    public static Task<GroupModel?> FindGroupAsync(this AppDbContext dbContext, string groupName)
    {
        return dbContext.Groups.FirstOrDefaultAsync(group => group.Name == groupName);
    }
}