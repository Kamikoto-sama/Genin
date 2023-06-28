using Microsoft.EntityFrameworkCore;
using Provider.Api.Data;
using Provider.Api.Data.Models;

namespace Provider.Api.Services;

public static class ZoneQueries
{
    public static Task<ZoneModel?> FindZoneAsync(this AppDbContext dbContext, string zoneName)
    {
        return dbContext.Zones.FirstOrDefaultAsync(zone => zone.Name == zoneName);
    }
}