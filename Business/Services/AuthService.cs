using System.Net.Http.Json;
using AutoMapper;
using Business.Exceptions;
using Common.Helpers;
using Business.Interfaces;
using Business.Models;
using Business.Models.Auth;
using Data.Interfaces;

namespace Business.Services;

public class AuthService(IHttpClientFactory httpClientFactory, IUserService userService, IMapper mapper, IUnitOfWork unitOfWork) : IAuthService
{
	public async Task<LoginSuccessResponseModel> LoginAsync(LoginRequestModel loginRequest)
	{
		if (!loginRequest.Model.InternalAuth)
		{
			var client = httpClientFactory.CreateClient("AuthClient");
			var msg = new
			{
				email = loginRequest.Model.Login,
				password = loginRequest.Model.Password
			};

			var response = await client.PostAsJsonAsync("api/auth/", msg);
			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadFromJsonAsync<LoginSuccessResponseModel>();

				var userEmail = content!.Email;
				var user = await userService.GetByEmailAsync(userEmail);

				if (user == null)
				{
					var newUser = new UserRegistrationModel
					{
						Email = content.Email,
						FirstName = content.FirstName,
						LastName = content.LastName,
						Password = loginRequest.Model.Password,
						ConfirmPassword = loginRequest.Model.Password,
					};
					await userService.AddAsync(newUser);
					var addedUser = await userService.GetByEmailAsync(userEmail);
					content.Id = addedUser.User.Id;
					content.Roles = addedUser.Roles;
				}

				return content;
			}

			var errorResponse = await response.Content.ReadFromJsonAsync<LoginErrorResponseModel>();
			throw new GameStoreValidationException($"Login failed: {errorResponse.Title}");
		}
		else
		{
			var user = await userService.GetByEmailWithRolesAsync(loginRequest.Model.Login) ?? throw new GameStoreValidationException("Login failed");
			var roles = mapper.Map<IEnumerable<RoleModel>>(user.Roles);
			if (user.User.IsBanned)
			{
				throw new GameStoreValidationException("User is banned");
			}

			if (!PasswordHasher.VerifyHashedPassword(user.User.PasswordHash, loginRequest.Model.Password))
			{
				throw new GameStoreValidationException("Invalid Password");
			}

			return new LoginSuccessResponseModel() { Id = user.User.Id ,Email = user.User.Email, FirstName = user.User.FirstName, LastName = user.User.LastName, Roles = roles};
		}
	}
}
