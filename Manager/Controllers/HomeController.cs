using Common.Api;
using Microsoft.AspNetCore.Mvc;
using Provider.Client;
using Provider.Dto.Zones;

namespace Manager.Controllers;

[Route("api")]
[ApiController]
public class HomeController : ApiController
{
    private readonly IProviderApiClient providerApiClient;

    public HomeController(IProviderApiClient providerApiClient)
    {
        this.providerApiClient = providerApiClient;
    }

    [HttpGet("zones")]
    public async Task<ActionResult> GetAllZones()
    {
        var response = await providerApiClient.GetAllZones();
        if (response.IsSuccessStatusCode)
            return Ok(response.Content ?? Array.Empty<ZoneDto>());
        return BadRequest(response.Error);
    }
}