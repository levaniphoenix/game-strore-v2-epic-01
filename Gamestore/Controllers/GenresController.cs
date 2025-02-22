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
	private readonly IGenreService _genreService = genreService;

	[HttpGet]
	public async Task<IEnumerable<GenreDetails>> Get()
	{
		return (await _genreService.GetAllAsync()).Select(g => g.Genre);
	}

	[HttpGet("{id}")]
	public async Task<GenreModel?> Get(Guid id)
	{
		return await _genreService.GetByIdAsync(id);
	}

	[HttpGet("{id}/games")]
	public async Task<IEnumerable<GameDetails?>> GetGamesByGenre(Guid id)
	{
		return (await _genreService.GetGamesByGenreIdAsync(id)).Select(g => g.Game);
	}

	[HttpGet("{id}/genres")]
	public async Task<IEnumerable<GenreDetails?>> GetGenresByParentId(Guid id)
	{
		var genres = await _genreService.GetGenresByParentId(id) ?? throw new GameStoreNotFoundException(ErrorMessages.GenreNotFound);
		return genres.Select(g => g.Genre);
	}

	[HttpPost]
	public async Task Post([FromBody] GenreModel genre)
	{
		if (!ModelState.IsValid)
		{
			throw new GameStoreModelStateException("Model is not valid");
		}

		await _genreService.AddAsync(genre);
	}

	[HttpPut]
	public async Task Put([FromBody] GenreModel genre)
	{
		if (!ModelState.IsValid)
		{
			throw new GameStoreModelStateException("Model is not valid");
		}

		await _genreService.UpdateAsync(genre);
	}

	[HttpDelete("{id}")]
	public async Task Delete(Guid id)
	{
		await _genreService.DeleteAsync(id);
	}
}
