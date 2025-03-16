using System.Text;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;
using static Business.Models.GenreModel;
using static Business.Models.PlatformModel;
using static Business.Models.PublisherModel;

namespace Gamestore.Controllers;

[Route("[controller]")]
[ApiController]
public class GamesController(IGameService gameService) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<IEnumerable<GameDetails>>> Get()
	{
		var games = (await gameService.GetAllAsync()).Select(x => x.Game);
		return Ok(games);
	}

	[HttpGet("{key}")]
	public async Task<ActionResult<GameDetails?>> Get(string key)
	{
		var game = await gameService.GetByKeyAsync(key);
		if (game is null)
		{
			return NotFound("could not find the game with the specified key");
		}

		return Ok(game.Game);
	}

	[HttpGet("{key}/genres")]
	public async Task<ActionResult<IEnumerable<GenreDetails>>> GetGenresByGamekey(string key)
	{
		var genres = (await gameService.GetGenresByGamekey(key)).Select(x => x.Genre);
		return Ok(genres);
	}

	[HttpGet("{key}/platforms")]
	public async Task<ActionResult<IEnumerable<PlatformDetails>>> GetPlatformsByGamekey(string key)
	{
		var platforms = (await gameService.GetPlatformsByGamekey(key)).Select(x => x.Platform);
		return Ok(platforms);
	}

	[HttpGet("{key}/publisher")]
	public async Task<ActionResult<PublisherDetails>> GetPublisherByGamekey(string key)
	{
		var publisher = await gameService.GetPublisherByGamekey(key);
		return Ok(publisher.Publisher);
	}

	[HttpGet("find/{id}")]
	public async Task<ActionResult<GameDetails?>> GetById(Guid id)
	{
		var game = await gameService.GetByIdAsync(id);

		if (game is null)
		{
			return NotFound("The game was not found");
		}

		return Ok(game.Game);
	}

	[HttpGet("{key}/file")]
	public async Task<ActionResult> GetFile(string key)
	{
		var game = await gameService.GetByKeyAsync(key);
		if (game is null)
		{
			return NotFound("game was not found");
		}

		var fileName = $"_{key}.txt";
		var fileContent = $"{game.Game.Name}\n{game.Game.Description}";
		var fileBytes = Encoding.UTF8.GetBytes(fileContent);

		return File(fileBytes, "text/plain", fileName);
	}

	// POST: games/
	[HttpPost]
	public async Task<ActionResult> Post([FromBody] GameModel game)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		await gameService.AddAsync(game);
		return Ok();
	}

	// PUT: games/
	[HttpPut]
	public async Task<ActionResult> Put([FromBody] GameModel game)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		await gameService.UpdateAsync(game);
		return Ok();
	}

	// DELETE: games/1
	[HttpDelete("{key}")]
	public async Task<ActionResult> Delete(string key)
	{
		await gameService.DeleteByKeyAsync(key);
		return Ok();
	}
}
