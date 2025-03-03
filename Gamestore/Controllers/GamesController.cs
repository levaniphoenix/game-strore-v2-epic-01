using System.Text;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

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
	public async Task<ActionResult<IEnumerable<GenreModel>>> GetGenresByGamekey(string key)
	{
		return Ok(await gameService.GetGenresByGamekey(key));
	}

	[HttpGet("{key}/platforms")]
	public async Task<ActionResult<IEnumerable<PlatformModel>>> GetPlatformsByGamekey(string key)
	{
		return Ok(await gameService.GetPlatformsByGamekey(key));
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
	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete(Guid id)
	{
		await gameService.DeleteAsync(id);
		return Ok();
	}
}
