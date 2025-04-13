using AutoMapper;
using Business.Exceptions;
using Common.Helpers;
using Business.Interfaces;
using Business.Models;
using Business.Models.Auth;
using Common.Options;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;
public class UserService(IUnitOfWork unitOfWork, IMapper mapper) : IUserService
{
	public Dictionary<string, DateTime> BannedUsers { get; set; } = [];

	public async Task AddAsync(UserModel model)
	{
		await ValidateUser(model);
		var user = mapper.Map<User>(model);
		var role = (await unitOfWork.RoleRepository.GetAllAsync(x => x.Name == "User")).Single();
		user.Roles = [role];
		await unitOfWork.UserRepository.AddAsync(user);
		await unitOfWork.SaveAsync();
	}

	public async Task UpdateAsync(UserModel model)
	{
		await ValidateUser(model);
		var user = mapper.Map<User>(model);
		unitOfWork.UserRepository.Update(user);
		await unitOfWork.SaveAsync();
	}

	public async Task UpdateAsync(UserDirectAddOrUpdateModel model)
	{
		var user = (await unitOfWork.UserRepository.GetAllAsync(x => x.Email == model.User.Name, includeProperties: "Roles")).FirstOrDefault() ?? throw new GameStoreValidationException("User not found");
		user.FirstName = model.User.Name;
		user.PasswordHash = PasswordHasher.HashPassword(model.Password);

		if (model.RoleIds != null)
		{
			var roles = await unitOfWork.RoleRepository.GetAllAsync(x => model.RoleIds.Contains(x.Id));
			if (roles.Count() != model.RoleIds.Length)
			{
				throw new GameStoreValidationException("Invalid role ids");
			}
			user.Roles = roles.ToList();
		}
		unitOfWork.UserRepository.Update(user);
		await unitOfWork.SaveAsync();
	}

	public async Task AddAsync(UserRegistrationModel model)
	{
		await ValidateUser(model);

		var user = mapper.Map<User>(model);
		user.PasswordHash = PasswordHasher.HashPassword(model.Password);

		//assinging default role
		var role = (await unitOfWork.RoleRepository.GetAllAsync(x => x.Name == "User")).Single();
		user.Roles = [role];

		await unitOfWork.UserRepository.AddAsync(user);
		await unitOfWork.SaveAsync();
	}

	public async Task AddAsync(UserDirectAddOrUpdateModel model)
	{
		var userRegistrationModel = new UserRegistrationModel
		{
			Email = model.User.Name,
			FirstName = model.User.Name,
			LastName = model.User.Name,
			Password = model.Password,
			ConfirmPassword = model.Password,
		};
		await ValidateUser(userRegistrationModel);

		//validate passed in role ids
		if (model.RoleIds != null)
		{
			var roles = await unitOfWork.RoleRepository.GetAllAsync(x => model.RoleIds.Contains(x.Id));
			if (roles.Count() != model.RoleIds.Length)
			{
				throw new GameStoreValidationException("Invalid role ids");
			}
		}

		//add user
		await AddAsync(userRegistrationModel);
		var user = (await unitOfWork.UserRepository.GetAllAsync(x => x.Email == model.User.Name)).FirstOrDefault() ?? throw new GameStoreValidationException("User not found");
		var dbRoles = await unitOfWork.RoleRepository.GetAllAsync(x => model.RoleIds!.Contains(x.Id));
		user.Roles = dbRoles.ToList();
		unitOfWork.UserRepository.Update(user);
		await unitOfWork.SaveAsync();
	}

	public async Task BanUser(BanRequestModel banRequest)
	{
		var existingUser = (await unitOfWork.UserRepository.GetAllAsync(x => x.Email == banRequest.UserName)).FirstOrDefault() ?? throw new GameStoreValidationException("User not found");
		
		existingUser.IsBanned = true;

		BannedUsers[banRequest.UserName] = banRequest.Duration switch
		{
			BanDurationOptions.OneHour => DateTime.Now.AddHours(1),
			BanDurationOptions.OneDay => DateTime.Now.AddDays(1),
			BanDurationOptions.OneWeek => DateTime.Now.AddDays(7),
			BanDurationOptions.OneMonth => DateTime.Now.AddMonths(1),
			BanDurationOptions.Permanent => DateTime.MaxValue,
			_ => throw new GameStoreValidationException("Invalid ban duration"),
		};

		unitOfWork.UserRepository.Update(existingUser);
		await unitOfWork.SaveAsync();
	}

	public async Task<UserModel?> GetByEmailAsync(string email)
	{
		var user = (await unitOfWork.UserRepository.GetAllAsync(x => x.Email == email)).FirstOrDefault();
		if (user == null)
		{
			return null;
		}
		return mapper.Map<UserModel>(user);
	}

	public async Task<UserModel?> GetByIdWithRolesAsync(Guid id)
	{
		var user = (await unitOfWork.UserRepository.GetAllAsync(x => x.Id == id, includeProperties: "Roles")).FirstOrDefault();
		if (user == null)
		{
			return null;
		}
		return mapper.Map<UserModel>(user);
	}

	public async Task<UserModel?> GetByEmailWithRolesAsync(string email)
	{
		var user = (await unitOfWork.UserRepository.GetAllAsync(x => x.Email == email, includeProperties: "Roles")).FirstOrDefault();
		if (user == null)
		{
			return null;
		}
		return mapper.Map<UserModel>(user);
	}

	public async Task DeleteAsync(object modelId)
	{
		await unitOfWork.UserRepository.DeleteByIdAsync(modelId);
		await unitOfWork.SaveAsync();
	}

	public async Task<IEnumerable<UserModel>> GetAllAsync()
	{
		var users = await unitOfWork.UserRepository.GetAllAsync();
		return mapper.Map<IEnumerable<UserModel>>(users);
	}

	public async Task<UserModel?> GetByIdAsync(object id)
	{
		var user =await unitOfWork.UserRepository.GetByIDAsync(id);
		return mapper.Map<UserModel>(user);
	}

	public async Task<bool> IsUserBanned(string userName)
	{
		var user = (await unitOfWork.UserRepository.GetAllAsync(x => x.Email == userName)).FirstOrDefault();
		
		if (user == null)
		{
			return false;
		}

		return user.IsBanned && user.BannedUntil > DateTime.Now;
	}

	private async Task ValidateUser(UserRegistrationModel model)
	{
		if (!model.Password.Equals(model.ConfirmPassword))
		{
			throw new GameStoreValidationException("Passwords do not match");
		}

		var existingUser = (await unitOfWork.UserRepository.GetAllAsync(x => x.Email == model.Email)).FirstOrDefault();

		if (existingUser != null)
		{
			throw new GameStoreValidationException("User with this email already exists");
		}
	}

	private async Task ValidateUser(UserModel model)
	{
		var existingUser = (await unitOfWork.UserRepository.GetAllAsync(x => x.Email == model.User.Email && x.Id != model.User.Id)).FirstOrDefault();

		if (existingUser != null)
		{
			throw new GameStoreValidationException("User with this email already exists");
		}
	}
}
