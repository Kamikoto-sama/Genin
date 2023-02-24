using System.ComponentModel.DataAnnotations;
using Common.Api;
using Microsoft.AspNetCore.Mvc;
using Provider.Api.Mappings;
using Provider.Api.Services;
using Provider.Api.Validations;
using Provider.Dto.Groups;

namespace Provider.Api.Controllers;

[Route("api/group/{groupName}")]
[Validate<GroupNameValidator, string>("groupName")]
public class GroupController : ApiController
{
    private readonly GroupService groupService;

    public GroupController(GroupService groupService)
    {
        this.groupService = groupService;
    }

    [HttpPost("create")]
    public Task<ActionResult<int>> Create(string groupName, int? parentId) =>
        HandleAsync(() => groupService.CreateAsync(groupName, parentId));

    [HttpGet]
    public Task<ActionResult<GroupDto>> Get(string groupName) =>
        HandleAsync(() => groupService.GetAsync(groupName), GroupMapper.ToGroupDto);

    [HttpPost("updateName")]
    public Task<ActionResult> UpdateName(string groupName, [Required] string newName) =>
        HandleAsync(() => groupService.UpdateNameAsync(groupName, newName));

    [HttpPost("setParent")]
    public Task<ActionResult> SetParent(string groupName, [Required] int parentId) =>
        HandleAsync(() => groupService.SetParentAsync(groupName, parentId));

    [HttpPost("removeParent")]
    public Task<ActionResult> RemoveParent(string groupName) =>
        HandleAsync(() => groupService.SetParentAsync(groupName, null));

    [HttpPost("delete")]
    public Task<ActionResult> Delete(string groupName) =>
        HandleAsync(() => groupService.DeleteAsync(groupName));
}