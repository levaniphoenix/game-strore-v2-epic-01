using System.Security.Claims;
using System.Text;
using Business.Interfaces;
using Business.Models;
using Common.Filters;
using Common.Options;
using Gamestore.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Business.Models.CommentModel;
using static Business.Models.GenreModel;
using static Business.Models.PlatformModel;
using static Business.Models.PublisherModel;

namespace Gamestore.Controllers;

[Route("[controller]")]
[ApiController]
public class GamesController(IGameService gameService, ICommentService commentService) : ControllerBase
{
	[AllowAnonymous]
	[HttpGet("all")]
	public async Task<ActionResult<IEnumerable<GameDetails>>> GetAll()
	{
		var userRoles = JwtHelper.GetUserRoles(HttpContext);
		IEnumerable<GameDetails> games = userRoles.Contains("Admin")
			? (await gameService.GetAllAsync(true)).Select(x => x.Game)
			: (await gameService.GetAllAsync(false)).Select(x => x.Game);
		return Ok(games);
	}

	[AllowAnonymous]
	[HttpGet]
	public async Task<ActionResult<PaginatedGamesModel>> Get([FromQuery] GameFilter filter)
	{
		var userRoles = JwtHelper.GetUserRoles(HttpContext);
		PaginatedGamesModel games = userRoles.Contains("Admin")
			? await gameService.GetAllWithFilterAsync(filter, true)
			: await gameService.GetAllWithFilterAsync(filter, false);

		return Ok(games);
	}

	[AllowAnonymous]
	[HttpGet("{key}")]
	public async Task<ActionResult<GameDetails?>> Get(string key)
	{
		var userRoles = JwtHelper.GetUserRoles(HttpContext);
		var game = userRoles.Contains("Admin")
			? await gameService.GetByKeyAsync(key, true)
			: await gameService.GetByKeyAsync(key, false);
		if (game is null)
		{
			return NotFound("could not find the game with the specified key");
		}

		return Ok(game.Game);
	}

	[AllowAnonymous]
	[HttpGet("{key}/genres")]
	public async Task<ActionResult<IEnumerable<GenreDetails>>> GetGenresByGamekey(string key)
	{
		var genres = (await gameService.GetGenresByGamekey(key)).Select(x => x.Genre).ToList();
		return Ok(genres);
	}

	[HttpGet("{key}/platforms")]
	public async Task<ActionResult<IEnumerable<PlatformDetails>>> GetPlatformsByGamekey(string key)
	{
		var platforms = (await gameService.GetPlatformsByGamekey(key)).Select(x => x.Platform);
		return Ok(platforms);
	}

	[AllowAnonymous]
	[HttpGet("{key}/publisher")]
	public async Task<ActionResult<PublisherDetails>> GetPublisherByGamekey(string key)
	{
		var publisher = await gameService.GetPublisherByGamekey(key);
		return Ok(publisher.Publisher);
	}

	[AllowAnonymous]
	[HttpGet("{key}/comments")]
	public async Task<ActionResult<IEnumerable<CommentDetails>>?> GetCommentsByGameKeyAsync(string key)
	{
		var comments = (await commentService.GetCommentsByGameKeyAsync(key)).Select(x => x.Comment);
		return Ok(comments);
	}

	[AllowAnonymous]
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

	[AllowAnonymous]
	[HttpGet("{key}/file")]
	public async Task<ActionResult> GetFile(string key)
	{
		var userRoles = JwtHelper.GetUserRoles(HttpContext);
		var game = userRoles.Contains("Admin")
			? await gameService.GetByKeyAsync(key, true)
			: await gameService.GetByKeyAsync(key, false);
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
	[Authorize(Policy = "ManagerPolicy")]
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
	[Authorize(Policy = "ManagerPolicy")]
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
	[Authorize(Policy = "ManagerPolicy")]
	[HttpDelete("{key}")]
	public async Task<ActionResult> Delete(string key)
	{
		await gameService.DeleteByKeyAsync(key);
		return Ok();
	}

	[Authorize(Policy = "UserPolicy")]
	[HttpPost("{key}/buy")]
	public async Task<ActionResult> AddGameToCart(string key)
	{
		var game = await gameService.GetByKeyAsync(key, true);
		if (game.Game.IsDeleted)
		{
			return BadRequest("The game is deleted");
		}

		var userId = JwtHelper.GetUserId(HttpContext);
		await gameService.AddToCartAsync(key, userId);
		return Ok();
	}

	[Authorize(Policy = "UserPolicy")]
	[HttpPost("{key}/comments")]
	public async Task<ActionResult> AddComment(string key, [FromBody] CommentModel comment)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var game = await gameService.GetByKeyAsync(key, false);

		if (game is null)
		{
			return NotFound("could not find the game with the specified key");
		}

		comment.GameId = game.Game.Id;
		comment.Comment.Name = User.FindFirst(ClaimTypes.Email).Value;

		switch (comment.Action)
		{
			case "Quote":
				await commentService.QuoteCommentAsync(comment.ParentId, comment.Comment.Name, comment.Comment.Body);
				break;
			case "Reply":
				await commentService.ReplyToCommentAsync(comment.ParentId, comment.Comment.Name, comment.Comment.Body);
				break;
			default:
				await commentService.AddAsync(comment);
				break;
		}

		return Ok();
	}

	[Authorize(Policy = "ManagerPolicy")]
	[HttpDelete("{key}/comments/{id}")]
	public async Task<ActionResult> DeleteComment(string key, Guid id)
	{
		var game = await gameService.GetByKeyAsync(key, true);

		if (game is null)
		{
			return NotFound("could not find the game with the specified key");
		}

		var comment = await commentService.GetByIdAsync(id);

		if (comment is null)
		{
			return NotFound("could not find the comment with the specified id");
		}

		await commentService.DeleteAsync(id);
		return Ok();
	}

	[AllowAnonymous]
	[HttpGet("publish-date-options")]
	public ActionResult<IEnumerable<string>> GetPublishDateOptions()
	{
		return Ok(PublishingDateOptions.Values);
	}

	[AllowAnonymous]
	[HttpGet("pagination-options")]
	public ActionResult<IEnumerable<string>> GetPaginationOptions()
	{
		return Ok(PaginationPageCountOptions.Values);
	}

	[AllowAnonymous]
	[HttpGet("sorting-options")]
	public ActionResult<IEnumerable<string>> GetSortingOptions()
	{
		return Ok(SortingOptions.Values);
	}
}
