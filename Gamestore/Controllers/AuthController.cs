using Business.Exceptions;
using Business.Interfaces;
using Business.Models.Auth;
using Gamestore.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.Controllers;

[Route("Users")]
[ApiController]
public class AuthController(IAuthService authService, TokenProvider tokenProvider) : ControllerBase
{
	[AllowAnonymous]
	[HttpPost("login")]
	public async Task<ActionResult<string>> Login([FromBody] LoginRequestModel loginRequest)
	{
		try
		{
			var result = await authService.LoginAsync(loginRequest);
			var token = tokenProvider.GenerateToken(result.Id.ToString(), result.Email, result.Roles);
			return Ok(new { token = token });
		}
		catch (GameStoreValidationException ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[AllowAnonymous]
	[HttpPost("access")]
	public ActionResult Access()
	{
		return Ok();
	}
}
