using Business.Interfaces;
using Business.Models;
using Business.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Business.Models.UserModel;

namespace Gamestore.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
	[Authorize(Policy = "ManagerPolicy")]
	[HttpGet]
	public async Task<ActionResult<IEnumerable<UserDetails>>> Get()
	{
		var users = await userService.GetAllAsync();
		return Ok(users.Select(x => x.User));
	}

	[Authorize(Policy = "ManagerPolicy")]
	[HttpGet("{id}")]
	public async Task<ActionResult<UserDetails>> GetById(Guid id)
	{
		var user = await userService.GetByIdAsync(id);
		if (user == null)
		{
			return NotFound();
		}

		return Ok(user.User);
	}

	[Authorize(Policy = "ManagerPolicy")]
	[HttpGet("{id}/roles")]
	public async Task<ActionResult<IEnumerable<string>>> GetRoles(Guid id)
	{
		UserModel user = await userService.GetByIdWithRolesAsync(id);
		if (user == null)
		{
			return NotFound();
		}

		var roleSummaries = user.Roles.Select(x => new
		{
			Id = x.Role.Id,
			Name = x.Role.Name,
		});
		return Ok(roleSummaries);
	}

	[AllowAnonymous]
	[HttpPost]
	public async Task<ActionResult> Post([FromBody] UserDirectAddOrUpdateModel model)
	{
		if (model == null)
		{
			return BadRequest();
		}

		await userService.AddAsync(model);
		return Created();
	}

	[Authorize(Policy = "ManagerPolicy")]
	[HttpPut]
	public async Task<ActionResult> Put([FromBody] UserDirectAddOrUpdateModel model)
	{
		if (model == null)
		{
			return BadRequest();
		}

		await userService.UpdateAsync(model);
		return Created();
	}

	[Authorize(Policy = "ManagerPolicy")]
	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete(Guid id)
	{
		await userService.DeleteAsync(id);
		return NoContent();
	}
}
