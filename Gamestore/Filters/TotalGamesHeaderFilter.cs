using Business.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Gamestore.Filters;

public class TotalGamesHeaderFilter(IGameService gameService) : IAsyncActionFilter
{
	public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
	{
		var resultContext = await next();

		// Get total games count
		int totalGamesCount = await gameService.GetTotalGamesCountAsync();

		// Add response header
		resultContext.HttpContext.Response.Headers["x-total-numbers-of-games"] = totalGamesCount.ToString();
	}
}
