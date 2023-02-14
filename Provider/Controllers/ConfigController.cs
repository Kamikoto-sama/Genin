using Microsoft.AspNetCore.Mvc;
using Provider.Dto;
using Provider.Services;

namespace Provider.Controllers;

[Route("api/{envName}/configs")]
public class ConfigController : Controller
{
    private readonly ConfigService configService;

    public ConfigController(ConfigService configService)
    {
        this.configService = configService;
    }

    [HttpPost("add")]
    public async Task<ActionResult> Add(string envName, [FromBody] AddConfigDto[] dtos)
    {
        await configService.AddAsync(envName, dtos);
        return Ok();
    }

    [HttpPost("updateValue")]
    public async Task<ActionResult> UpdateValue(string envName, [FromBody] AddConfigDto dto)
    {
        await configService.UpdateValueAsync(envName, dto);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult> Get(string envName, [FromBody] string[] keys)
    {
        var configs = await configService.GetAsync(envName, keys);
        return Ok(configs);
    }

    [HttpPost("delete")]
    public async Task<ActionResult> Delete(string envName, [FromBody] string[] keys)
    {
        await configService.DeleteAsync(envName, keys);
        return Ok();
    }
}