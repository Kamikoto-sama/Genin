using Microsoft.AspNetCore.Mvc;
using Provider.Dto;
using Provider.Services;

namespace Provider.Controllers;

[Route("api/environment")]
public class EnvironmentController : Controller
{
    private readonly EnvironmentService environmentService;

    public EnvironmentController(EnvironmentService environmentService)
    {
        this.environmentService = environmentService;
    }

    [HttpPost("create")]
    public async Task<ActionResult> Create([FromBody] EnvCreateDto dto)
    {
        var envId = await environmentService.CreateAsync(dto.Name, dto.ParentId);
        return Ok(envId);
    }

    [HttpGet("{envName}")]
    public async Task<ActionResult> Get(string envName)
    {
        var env = await environmentService.GetAsync(envName);
        if (env == null)
            return NotFound();
        return Ok(new EnvDto
        {
            Id = env.Id,
            Name = env.Name,
            ParentName = env.Parent?.Name
        });
    }

    [HttpPost("updateName")]
    public async Task<ActionResult> UpdateName([FromBody] EnvNameUpdateDto dto)
    {
        await environmentService.UpdateNameAsync(dto.Name, dto.NewName);
        return Ok();
    }

    [HttpPost("setParent")]
    public async Task<ActionResult> SetParent([FromBody] EnvSetParentDto dto)
    {
        await environmentService.SetParentAsync(dto.Name, dto.ParentId);
        return Ok();
    }

    [HttpPost("{envName}/delete")]
    public async Task<ActionResult> Delete(string envName)
    {
        await environmentService.DeleteAsync(envName);
        return Ok();
    }
}