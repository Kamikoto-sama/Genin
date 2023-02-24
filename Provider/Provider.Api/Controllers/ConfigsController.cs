using Common.Api;
using Microsoft.AspNetCore.Mvc;
using Provider.Api.Services;
using Provider.Api.Validations;
using Provider.Dto.Configs;

namespace Provider.Api.Controllers;

[Route("api/{groupName}/configs")]
[Validate<GroupNameValidator, string>("groupName")]
public class ConfigsController : ApiController
{
    private readonly ConfigService configService;

    public ConfigsController(ConfigService configService)
    {
        this.configService = configService;
    }

    [HttpPost("add")]
    [Validate<ConfigAddValidator, ConfigAddDto[]>(nameof(dto))]
    public Task<ActionResult> Add(string groupName, [FromBody] ConfigAddDto[] dto) =>
        HandleAsync(() => configService.AddAsync(groupName, dto));

    [HttpPost("updateValue")]
    public Task<ActionResult> UpdateValue(string groupName, [FromBody] ConfigUpdateDto dto) =>
        HandleAsync(() => configService.UpdateValueAsync(groupName, dto));

    [HttpGet, HttpPost]
    public Task<ActionResult<ConfigDto[]>> Get(string groupName, [FromBody] string[] keys) =>
        HandleAsync(() => configService.GetAsync(groupName, keys));

    [HttpPost("delete")]
    public Task<ActionResult> Delete(string groupName, [FromBody] string[] keys) =>
        HandleAsync(() => configService.DeleteAsync(groupName, keys));
}