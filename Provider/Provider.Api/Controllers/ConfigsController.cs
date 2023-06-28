using Common.Api;
using Microsoft.AspNetCore.Mvc;
using Provider.Api.Services;
using Provider.Api.Validations;
using Provider.Dto.Configs;

namespace Provider.Api.Controllers;

[Route("api/{zoneName}/configs")]
[Validate<ZoneNameValidator, string>("zoneName")]
public class ConfigsController : ApiController
{
    private readonly ConfigService configService;

    public ConfigsController(ConfigService configService)
    {
        this.configService = configService;
    }

    [HttpPost("add")]
    [Validate<ConfigAddValidator, ConfigAddDto[]>(nameof(dto))]
    public Task<ActionResult> Add(string zoneName, [FromBody] ConfigAddDto[] dto) =>
        HandleAsync(() => configService.AddAsync(zoneName, dto));

    [HttpPost("updateValue")]
    public Task<ActionResult> UpdateValue(string zoneName, [FromBody] ConfigUpdateDto dto) =>
        HandleAsync(() => configService.UpdateValueAsync(zoneName, dto));

    [HttpGet, HttpPost]
    public Task<ActionResult<ConfigDto[]>> Get(string zoneName, [FromBody] string[] keys) =>
        HandleAsync(() => configService.GetAsync(zoneName, keys));

    [HttpPost("delete")]
    public Task<ActionResult> Delete(string zoneName, [FromBody] string[] keys) =>
        HandleAsync(() => configService.DeleteAsync(zoneName, keys));
}