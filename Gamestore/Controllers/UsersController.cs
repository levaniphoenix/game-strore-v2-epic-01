﻿using Business.Exceptions;
using Business.Interfaces;
using Business.Models.Auth;
using Gamestore.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController(IAuthService authService, TokenProvider tokenProvider) : ControllerBase
{
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

	[HttpPost("access")]
	public ActionResult Access()
	{
		return Ok();
	}
}
