using Microsoft.AspNetCore.Mvc;
using Provider.Dto;
using Provider.Services;

namespace Provider.Controllers;

[Route("api/group")]
public class GroupController : Controller
{
    private readonly GroupService groupService;

    public GroupController(GroupService groupService)
    {
        this.groupService = groupService;
    }

    [HttpPost("create")]
    public async Task<ActionResult> Create([FromBody] GroupCreateDto dto)
    {
        var groupId = await groupService.CreateAsync(dto.Name, dto.ParentId);
        return Ok(groupId);
    }

    [HttpGet("{groupName}")]
    public async Task<ActionResult> Get(string groupName)
    {
        var group = await groupService.GetAsync(groupName);
        if (group == null)
            return NotFound();
        return Ok(new GroupDto
        {
            Id = group.Id,
            Name = group.Name,
            ParentName = group.Parent?.Name
        });
    }

    [HttpPost("updateName")]
    public async Task<ActionResult> UpdateName([FromBody] GroupNameUpdateDto dto)
    {
        await groupService.UpdateNameAsync(dto.Name, dto.NewName);
        return Ok();
    }

    [HttpPost("setParent")]
    public async Task<ActionResult> SetParent([FromBody] GroupSetParentDto dto)
    {
        await groupService.SetParentAsync(dto.Name, dto.ParentId);
        return Ok();
    }

    [HttpPost("{groupName}/delete")]
    public async Task<ActionResult> Delete(string groupName)
    {
        await groupService.DeleteAsync(groupName);
        return Ok();
    }
}