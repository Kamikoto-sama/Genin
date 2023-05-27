using Common.Api;
using Microsoft.AspNetCore.Mvc;
using Provider.Client;
using Provider.Dto.Groups;

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

    [HttpGet("groups")]
    public async Task<ActionResult> GetAllGroups()
    {
        var response = await providerApiClient.GetAllGroups();
        if (response.IsSuccessStatusCode)
            return Ok(response.Content ?? Array.Empty<GroupDto>());
        return BadRequest(response.Error);
    }
}