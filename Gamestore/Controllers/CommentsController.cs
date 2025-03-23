using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.Controllers;

[Route("[controller]")]
[ApiController]
public class CommentsController(ICommentService commentService) : ControllerBase
{
	[HttpGet("ban/durations")]
	public ActionResult<IEnumerable<string>> GetBanDurations()
	{
		return Ok(BanDurationsModel.BanDurations);
	}

	[HttpPost("ban")]
	public async Task<ActionResult> Post([FromBody] BanRequestModel banRequest)
	{
		await commentService.BanUserAsync(banRequest);
		return Ok();
	}
}
