using Microsoft.AspNetCore.Mvc;
using Provider.Dto;
using Provider.Services;

namespace Provider.Controllers;

[Route("api/{groupName}/configs")]
public class ConfigController : Controller
{
    private readonly ConfigService configService;

    public ConfigController(ConfigService configService)
    {
        this.configService = configService;
    }

    [HttpPost("add")]
    public async Task<ActionResult> Add(string groupName, [FromBody] AddConfigDto[] dtos)
    {
        await configService.AddAsync(groupName, dtos);
        return Ok();
    }

    [HttpPost("updateValue")]
    public async Task<ActionResult> UpdateValue(string groupName, [FromBody] AddConfigDto dto)
    {
        await configService.UpdateValueAsync(groupName, dto);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult> Get(string groupName, [FromBody] string[] keys)
    {
        var configs = await configService.GetAsync(groupName, keys);
        return Ok(configs);
    }

    [HttpPost("delete")]
    public async Task<ActionResult> Delete(string groupName, [FromBody] string[] keys)
    {
        await configService.DeleteAsync(groupName, keys);
        return Ok();
    }
}