using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Business.Models.PlatformModel;

namespace Gamestore.Controllers;

[Route("[controller]")]
[ApiController]
public class PlatformsController(IPlatformService platformService) : ControllerBase
{
	[AllowAnonymous]
	[HttpGet]
	public async Task<ActionResult<IEnumerable<PlatformDetails>>> Get()
	{
		return Ok((await platformService.GetAllAsync()).Select(p => p.Platform));
	}

	[AllowAnonymous]
	[HttpGet("{id}")]
	public async Task<ActionResult<PlatformDetails?>> Get(Guid id)
	{
		var platform = await platformService.GetByIdAsync(id);
		if (platform is null)
		{
			return NotFound("platform not found");
		}

		return Ok(platform.Platform);
	}

	[AllowAnonymous]
	[HttpGet("{id}/games")]
	public async Task<ActionResult<IEnumerable<GameDetails?>>> GetGamesByPlatform(Guid id)
	{
		return Ok((await platformService.GetGamesByPlatformIdAsync(id)).Select(g => g.Game));
	}

	[Authorize(Policy = "ManagerPolicy")]
	[HttpPost]
	public async Task<ActionResult> Post([FromBody] PlatformModel platform)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		await platformService.AddAsync(platform);
		return Ok();
	}

	[Authorize(Policy = "ManagerPolicy")]
	[HttpPut]
	public async Task<ActionResult> Put([FromBody] PlatformModel platform)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		await platformService.UpdateAsync(platform);
		return Ok();
	}

	[Authorize(Policy = "ManagerPolicy")]
	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete(Guid id)
	{
		await platformService.DeleteAsync(id);
		return Ok();
	}
}
