using Common.Results;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Provider.Api.Data;
using Provider.Api.Data.Models;
using Provider.Api.Mappings;

namespace Provider.Api.Services;

public class ZoneService
{
    private readonly AppDbContext dbContext;

    public ZoneService(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result> Create(string zoneName, string? parentName)
    {
        var zone = await dbContext.FindZoneAsync(zoneName);
        if (zone != null)
            return ZoneError.AlreadyExists();

        var parentResult = await GetParent(parentName);
        if (parentResult.IsFailed)
            return parentResult.ToResult();

        zone = ZoneMapper.ToZoneModel(zoneName, parentResult.Value);
        await dbContext.Zones.AddAsync(zone);
        await dbContext.SaveChangesAsync();

        return Result.Ok();
    }

    public async Task<Result> SetParent(string zoneName, string? parentName)
    {
        var parentResult = await GetParent(parentName);
        if (parentResult.IsFailed)
            return parentResult.ToResult();

        var zone = await dbContext.FindZoneAsync(zoneName);
        if (zone == null)
            return ZoneError.NotFound();

        zone.Parent = parentResult.Value;
        await dbContext.SaveChangesAsync();

        return Result.Ok();
    }

    private async Task<Result<ZoneModel?>> GetParent(string? parentName)
    {
        ZoneModel? parent = null;
        if (parentName == null)
            return parent;

        parent = await dbContext.Zones.SingleOrDefaultAsync(g => g.Name == parentName);
        if (parent == null)
            return ZoneError.ParentNotFound();
        return parent;
    }

    public async Task<Result<ZoneModel>> Get(string zoneName)
    {
        var zone = await dbContext.FindZoneAsync(zoneName);
        return zone == null ? ZoneError.NotFound() : zone;
    }

    public async Task<Result> UpdateName(string zoneName, string newName)
    {
        var zone = await dbContext.FindZoneAsync(zoneName);
        if (zone == null)
            return ZoneError.NotFound();

        if (await dbContext.FindZoneAsync(newName) != null)
            return ZoneError.AlreadyExists();

        zone.Name = newName;
        await dbContext.SaveChangesAsync();

        return Result.Ok();
    }

    public async Task<Result> Delete(string zoneName)
    {
        var zone = await dbContext.FindZoneAsync(zoneName);
        if (zone == null)
            return ZoneError.NotFound();

        dbContext.Zones.Remove(zone);
        await dbContext.SaveChangesAsync();

        return Result.Ok();
    }

    public async Task<Result<ZoneModel[]>> GetAll()
    {
        var zones = await dbContext.Zones.ToArrayAsync();
        return zones;
    }
}

public class ZoneError
{
    public enum Code
    {
        ZoneNotFound = 1,
        ZoneAlreadyExists,
        ParentNotFound
    }

    public static Result NotFound() => ResultHelper.NotFound(Code.ZoneNotFound);

    public static Result AlreadyExists() => ResultHelper.InvalidOperation(Code.ZoneAlreadyExists);

    public static Result ParentNotFound() => ResultHelper.InvalidOperation(Code.ParentNotFound);
}