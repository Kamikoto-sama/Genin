using System.ComponentModel.DataAnnotations;
using Common.Api;
using Microsoft.AspNetCore.Mvc;
using Provider.Api.Data.Models;
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
        HandleAsync(() => groupService.CreateAsync(name, parentName));

    [Validate<GroupNameValidator, string>(nameof(groupName))]
    [HttpGet("{groupName}")]
    public Task<ActionResult<GroupDto>> Get(string groupName) =>
        HandleAsync(() => groupService.GetAsync(groupName), GroupMapper.ToGroupDto);

    [HttpGet("all")]
    public Task<ActionResult<GroupModel[]>> GetAll() =>
        HandleAsync(() => groupService.GetAllAsync());

    [Validate<GroupNameValidator, string>(nameof(groupName))]
    [HttpPost("{groupName}/updateName")]
    public Task<ActionResult> UpdateName(string groupName, [Required] string newName) =>
        HandleAsync(() => groupService.UpdateNameAsync(groupName, newName));

    [Validate<GroupNameValidator, string>(nameof(groupName))]
    [HttpPost("{groupName}/setParent")]
    public Task<ActionResult> SetParent(string groupName, [Required] string parentName) =>
        HandleAsync(() => groupService.SetParentAsync(groupName, parentName));

    [Validate<GroupNameValidator, string>(nameof(groupName))]
    [HttpPost("{groupName}/removeParent")]
    public Task<ActionResult> RemoveParent(string groupName) =>
        HandleAsync(() => groupService.SetParentAsync(groupName, null));

    [Validate<GroupNameValidator, string>(nameof(groupName))]
    [HttpPost("{groupName}/delete")]
    public Task<ActionResult> Delete(string groupName) =>
        HandleAsync(() => groupService.DeleteAsync(groupName));
}