using Business.Exceptions;
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
	public async Task<IEnumerable<PlatformDetails>> Get()
	{
		return (await platformService.GetAllAsync()).Select(p => p.Platform);
	}

	[HttpGet("{id}")]
	public async Task<PlatformDetails?> Get(Guid id)
	{
		var platform = (await platformService.GetByIdAsync(id)) ?? throw new GameStoreNotFoundException(ErrorMessages.PlatformNotFound);
		return platform.Platform;
	}

	[HttpGet("{id}/games")]
	public async Task<IEnumerable<GameDetails?>> GetGamesByPlatform(Guid id)
	{
		return (await platformService.GetGamesByGenreIdAsync(id)).Select(g => g.Game);
	}

	[HttpPost]
	public async Task Post([FromBody] PlatformModel platform)
	{
		if (!ModelState.IsValid)
		{
			throw new GameStoreModelStateException("Model is not valid");
		}

		await platformService.AddAsync(platform);
	}

	[HttpPut]
	public async Task Put([FromBody] PlatformModel platform)
	{
		if (!ModelState.IsValid)
		{
			throw new GameStoreModelStateException("Model is not valid");
		}

		await platformService.UpdateAsync(platform);
	}

	[HttpDelete("{id}")]
	public async Task Delete(Guid id)
	{
		await platformService.DeleteAsync(id);
	}
}
