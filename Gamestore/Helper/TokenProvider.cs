using System.Security.Claims;
using System.Text;
using Business.Models;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Gamestore.Helper;

public class TokenProvider(IConfiguration configuration)
{
	public string GenerateToken(string userId, string email, IEnumerable<RoleModel> roles)
	{
		var tokenHandler = new JsonWebTokenHandler();
		var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]!);
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new[]
			{
				new Claim(ClaimTypes.NameIdentifier, userId),
				new Claim(ClaimTypes.Email, email),
			}),
			Expires = DateTime.UtcNow.AddHours(1),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
		};

		foreach (var role in roles)
		{
			tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role.Role.Name));
		}

		var token = tokenHandler.CreateToken(tokenDescriptor);
		return token;
	}
}
