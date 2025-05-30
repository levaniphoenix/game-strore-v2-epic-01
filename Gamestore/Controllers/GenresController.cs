﻿using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Business.Models.GenreModel;

namespace Gamestore.Controllers;

[Route("[controller]")]
[ApiController]
public class GenresController(IGenreService genreService) : ControllerBase
{
	[AllowAnonymous]
	[HttpGet]
	public async Task<ActionResult<IEnumerable<GenreDetails>>> Get()
	{
		return Ok((await genreService.GetAllAsync()).Select(g => g.Genre));
	}

	[AllowAnonymous]
	[HttpGet("{id}")]
	public async Task<ActionResult<GenreDetails?>> Get(Guid id)
	{
		var genre = await genreService.GetByIdAsync(id);

		if (genre is null)
		{
			return NotFound("genre was not found");
		}

		return Ok(genre.Genre);
	}

	[AllowAnonymous]
	[HttpGet("{id}/games")]
	public async Task<ActionResult<IEnumerable<GameDetails?>>> GetGamesByGenre(Guid id)
	{
		return Ok((await genreService.GetGamesByGenreIdAsync(id)).Select(g => g.Game));
	}

	[AllowAnonymous]
	[HttpGet("{id}/genres")]
	public async Task<ActionResult<IEnumerable<GenreDetails?>>> GetGenresByParentId(Guid id)
	{
		var genres = await genreService.GetGenresByParentId(id);
		if (!genres.Any())
		{
			return Ok();
		}

		return Ok(genres.Select(g => g.Genre));
	}

	[Authorize(Policy = "ManagerPolicy")]
	[HttpPost]
	public async Task<ActionResult> Post([FromBody] GenreModel genre)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		await genreService.AddAsync(genre);
		return Created();
	}

	[Authorize(Policy = "ManagerPolicy")]
	[HttpPut]
	public async Task<ActionResult> Put([FromBody] GenreModel genre)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		await genreService.UpdateAsync(genre);
		return Ok();
	}

	[Authorize(Policy = "ManagerPolicy")]
	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete(Guid id)
	{
		await genreService.DeleteAsync(id);
		return Ok();
	}
}
