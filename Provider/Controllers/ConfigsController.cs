using Microsoft.AspNetCore.Mvc;
using Provider.Dto.Configs;
using Provider.Services;

namespace Provider.Controllers;

[Route("api/{groupName}/configs")]
public class ConfigsController : ApiController
{
    private readonly ConfigService configService;

    public ConfigsController(ConfigService configService)
    {
        this.configService = configService;
    }

    [HttpPost("add")]
    public Task<ActionResult> Add(string groupName, [FromBody] AddConfigDto[] dtos) =>
        HandleAsync(() => configService.AddAsync(groupName, dtos));

    [HttpPost("updateValue")]
    public Task<ActionResult> UpdateValue(string groupName, [FromBody] UpdateConfigDto dtos) =>
        HandleAsync(() => configService.UpdateValueAsync(groupName, dtos));

    [HttpGet]
    public Task<ActionResult<ConfigDto[]>> Get(string groupName, [FromBody] string[] keys) =>
        HandleAsync(() => configService.GetAsync(groupName, keys));

    [HttpPost("delete")]
    public Task<ActionResult> Delete(string groupName, [FromBody] string[] keys) =>
        HandleAsync(() => configService.DeleteAsync(groupName, keys));
}