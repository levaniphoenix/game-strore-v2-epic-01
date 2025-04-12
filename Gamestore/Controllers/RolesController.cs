using Business.Interfaces;
using Business.Models;
using Common.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Business.Models.RoleModel;

namespace Gamestore.Controllers;

[Authorize(Policy ="ManagerPolicy")]
[Route("[controller]")]
[ApiController]
public class RolesController(IRoleService roleService) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<IEnumerable<RoleDetails>>> GetAll()
	{
		var roles = await roleService.GetAllAsync();
		return Ok(roles.Select(x => x.Role));
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<RoleDetails>> GetById(Guid id)
	{
		var role = await roleService.GetByIdAsync(id);
		if (role == null)
		{
			return NotFound();
		}

		return Ok(role.Role);
	}

	[HttpGet("permissions")]
	public ActionResult<IEnumerable<string>> GetPermissions()
	{
		var permissions = RolePermissions.Values;
		return Ok(permissions);
	}

	[HttpGet("{id}/permissions")]
	public async Task<ActionResult<IEnumerable<string>>> GetPermissions(Guid id)
	{
		var role = await roleService.GetByIdAsync(id);
		if (role == null)
		{
			return NotFound();
		}

		return Ok(role.Permissions);
	}

	[HttpPost]
	public async Task<ActionResult> Post([FromBody] RoleModel role)
	{
		await roleService.AddAsync(role);
		return Created();
	}

	[HttpPut]
	public async Task<ActionResult> Put([FromBody] RoleModel role)
	{
		await roleService.UpdateAsync(role);
		return NoContent();
	}
}
