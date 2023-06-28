using System.ComponentModel.DataAnnotations;
using Common.Api;
using Microsoft.AspNetCore.Mvc;
using Provider.Api.Mappings;
using Provider.Api.Services;
using Provider.Api.Validations;
using Provider.Dto.Zones;

namespace Provider.Api.Controllers;

[Route("api/zones")]
public class ZonesController : ApiController
{
    private readonly ZoneService zoneService;

    public ZonesController(ZoneService zoneService)
    {
        this.zoneService = zoneService;
    }

    [Validate<ZoneNameValidator, string>(nameof(name))]
    [HttpPost("create/{name}")]
    public Task<ActionResult> Create(string name, string? parentName) =>
        HandleAsync(() => zoneService.Create(name, parentName));

    [Validate<ZoneNameValidator, string>(nameof(zoneName))]
    [HttpGet("{zoneName}")]
    public Task<ActionResult<ZoneDto>> Get(string zoneName) =>
        HandleAsync(() => zoneService.Get(zoneName), ZoneMapper.ToZoneDto);

    [HttpGet("all")]
    public Task<ActionResult<ZoneDto[]>> GetAll() =>
        HandleAsync(() => zoneService.GetAll(), ZoneMapper.ToZoneDto);

    [Validate<ZoneNameValidator, string>(nameof(zoneName))]
    [HttpPost("{zoneName}/updateName")]
    public Task<ActionResult> UpdateName(string zoneName, [Required] string newName) =>
        HandleAsync(() => zoneService.UpdateName(zoneName, newName));

    [Validate<ZoneNameValidator, string>(nameof(zoneName))]
    [HttpPost("{zoneName}/setParent")]
    public Task<ActionResult> SetParent(string zoneName, [Required] string parentName) =>
        HandleAsync(() => zoneService.SetParent(zoneName, parentName));

    [Validate<ZoneNameValidator, string>(nameof(zoneName))]
    [HttpPost("{zoneName}/removeParent")]
    public Task<ActionResult> RemoveParent(string zoneName) =>
        HandleAsync(() => zoneService.SetParent(zoneName, null));

    [Validate<ZoneNameValidator, string>(nameof(zoneName))]
    [HttpPost("{zoneName}/delete")]
    public Task<ActionResult> Delete(string zoneName) =>
        HandleAsync(() => zoneService.Delete(zoneName));
}