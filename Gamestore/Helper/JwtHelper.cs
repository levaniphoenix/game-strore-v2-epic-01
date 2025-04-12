using System.Security.Claims;

namespace Gamestore.Helper;

public static class JwtHelper
{
	public static List<string> GetUserRoles(HttpContext httpContext)
	{
		var roles = new List<string>();

		if (httpContext.User.Identity?.IsAuthenticated == true)
		{
			roles = httpContext.User
				.Claims
				.Where(c => c.Type is ClaimTypes.Role or "role" or "roles")
				.Select(c => c.Value)
				.ToList();
		}

		return roles;
	}

	public static Guid GetUserId(HttpContext httpContext)
	{
		if (httpContext.User.Identity is ClaimsIdentity identity)
		{
			var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
			if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
			{
				return userId;
			}
		}

		throw new InvalidOperationException("User ID not found in JWT token.");
	}
}
