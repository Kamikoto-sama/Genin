using System.ComponentModel.DataAnnotations;
using Common.Api;
using Microsoft.AspNetCore.Mvc;
using Provider.Api.Mappings;
using Provider.Api.Services;
using Provider.Api.Validations;
using Provider.Dto.Groups;

namespace Provider.Api.Controllers;

[Route("api/groups")]
public class GroupsController : ApiController
{
    private readonly GroupService groupService;

    public GroupsController(GroupService groupService)
    {
        this.groupService = groupService;
    }

    [Validate<GroupNameValidator, string>(nameof(name))]
    [HttpPost("create/{name}")]
    public Task<ActionResult> Create(string name, string? parentName) =>
        HandleAsync(() => groupService.Create(name, parentName));

    [Validate<GroupNameValidator, string>(nameof(groupName))]
    [HttpGet("{groupName}")]
    public Task<ActionResult<GroupDto>> Get(string groupName) =>
        HandleAsync(() => groupService.Get(groupName), GroupMapper.ToGroupDto);

    [HttpGet("all")]
    public Task<ActionResult<GroupDto[]>> GetAll() =>
        HandleAsync(() => groupService.GetAll(), GroupMapper.ToGroupDto);

    [Validate<GroupNameValidator, string>(nameof(groupName))]
    [HttpPost("{groupName}/updateName")]
    public Task<ActionResult> UpdateName(string groupName, [Required] string newName) =>
        HandleAsync(() => groupService.UpdateName(groupName, newName));

    [Validate<GroupNameValidator, string>(nameof(groupName))]
    [HttpPost("{groupName}/setParent")]
    public Task<ActionResult> SetParent(string groupName, [Required] string parentName) =>
        HandleAsync(() => groupService.SetParent(groupName, parentName));

    [Validate<GroupNameValidator, string>(nameof(groupName))]
    [HttpPost("{groupName}/removeParent")]
    public Task<ActionResult> RemoveParent(string groupName) =>
        HandleAsync(() => groupService.SetParent(groupName, null));

    [Validate<GroupNameValidator, string>(nameof(groupName))]
    [HttpPost("{groupName}/delete")]
    public Task<ActionResult> Delete(string groupName) =>
        HandleAsync(() => groupService.Delete(groupName));
}