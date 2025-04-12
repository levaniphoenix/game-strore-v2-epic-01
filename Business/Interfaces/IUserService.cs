using Business.Models;
using Business.Models.Auth;

namespace Business.Interfaces;

public interface IUserService : ICrud<UserModel>
{
	Task<UserModel?> GetByEmailAsync(string email);

	Task<UserModel?> GetByEmailWithRolesAsync(string email);

	Task AddAsync(UserRegistrationModel model);

	Task BanUser(BanRequestModel banRequest);

	Task<bool> IsUserBanned(string userName);
}
