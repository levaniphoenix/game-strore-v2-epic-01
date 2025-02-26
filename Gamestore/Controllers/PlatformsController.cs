using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;
using static Business.Models.PlatformModel;

namespace Gamestore.Controllers;

[Route("[controller]")]
[ApiController]
public class PlatformsController(IPlatformService platformService) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<IEnumerable<PlatformDetails>>> Get()
	{
		return Ok((await platformService.GetAllAsync()).Select(p => p.Platform));
	}

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

	[HttpGet("{id}/games")]
	public async Task<ActionResult<IEnumerable<GameDetails?>>> GetGamesByPlatform(Guid id)
	{
		return Ok((await platformService.GetGamesByGenreIdAsync(id)).Select(g => g.Game));
	}

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

	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete(Guid id)
	{
		await platformService.DeleteAsync(id);
		return Ok();
	}
}
