namespace Gamestore.Middleware;

public class FixAuthorizationHeaderMiddleware(RequestDelegate next)
{
	public async Task InvokeAsync(HttpContext context)
	{
		if (context.Request.Headers.TryGetValue("Authorization", out var authHeader))
		{
			var authValue = authHeader.ToString();
			if (!authValue.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
			{
				// Prepend "Bearer "
				context.Request.Headers.Authorization = $"Bearer {authValue}";
			}
		}

		await next(context);
	}
}
