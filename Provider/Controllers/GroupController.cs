using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Provider.Dto;
using Provider.Dto.Groups;
using Provider.Mappings;
using Provider.Services;

namespace Provider.Controllers;

[Route("api/group")]
public class GroupController : ApiController
{
    private readonly GroupService groupService;

    public GroupController(GroupService groupService)
    {
        this.groupService = groupService;
    }

    [HttpPost("create")]
    public Task<ActionResult<int>> Create([FromBody] GroupCreateDto dto) =>
        HandleAsync(() => groupService.CreateAsync(dto.Name, dto.ParentId));

    [HttpGet("{groupName}")]
    public Task<ActionResult<GroupDto>> Get(string groupName) =>
        HandleAsync(() => groupService.GetAsync(groupName), GroupMapper.ToGroupDto);

    [HttpPost("{groupName}/updateName")]
    public Task<ActionResult> UpdateName(string groupName, [Required] string newName) =>
        HandleAsync(() => groupService.UpdateNameAsync(groupName, newName));

    [HttpPost("{groupName}/setParent")]
    public Task<ActionResult> SetParent(string groupName, [Required] int parentId) =>
        HandleAsync(() => groupService.SetParentAsync(groupName, parentId));

    [HttpPost("{groupName}/removeParent")]
    public Task<ActionResult> RemoveParent(string groupName) =>
        HandleAsync(() => groupService.SetParentAsync(groupName, null));

    [HttpPost("{groupName}/delete")]
    public Task<ActionResult> Delete(string groupName) =>
        HandleAsync(() => groupService.DeleteAsync(groupName));
}