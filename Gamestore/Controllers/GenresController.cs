using Business.Exceptions;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;
using static Business.Models.GenreModel;

namespace Gamestore.Controllers;

[Route("[controller]")]
[ApiController]
public class GenresController(IGenreService genreService) : ControllerBase
{
	[HttpGet]
	public async Task<IEnumerable<GenreDetails>> Get()
	{
		return (await genreService.GetAllAsync()).Select(g => g.Genre);
	}

	[HttpGet("{id}")]
	public async Task<GenreModel?> Get(Guid id)
	{
		return await genreService.GetByIdAsync(id);
	}

	[HttpGet("{id}/games")]
	public async Task<IEnumerable<GameDetails?>> GetGamesByGenre(Guid id)
	{
		return (await genreService.GetGamesByGenreIdAsync(id)).Select(g => g.Game);
	}

	[HttpGet("{id}/genres")]
	public async Task<IEnumerable<GenreDetails?>> GetGenresByParentId(Guid id)
	{
		var genres = await genreService.GetGenresByParentId(id) ?? throw new GameStoreNotFoundException(ErrorMessages.GenreNotFound);
		return genres.Select(g => g.Genre);
	}

	[HttpPost]
	public async Task Post([FromBody] GenreModel genre)
	{
		if (!ModelState.IsValid)
		{
			throw new GameStoreModelStateException("Model is not valid");
		}

		await genreService.AddAsync(genre);
	}

	[HttpPut]
	public async Task Put([FromBody] GenreModel genre)
	{
		if (!ModelState.IsValid)
		{
			throw new GameStoreModelStateException("Model is not valid");
		}

		await genreService.UpdateAsync(genre);
	}

	[HttpDelete("{id}")]
	public async Task Delete(Guid id)
	{
		await genreService.DeleteAsync(id);
	}
}
