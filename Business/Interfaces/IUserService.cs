using Business.Models;
using Business.Models.Auth;

namespace Business.Interfaces;

public interface IUserService
{
	Task DeleteAsync(object modelId);

	Task<IEnumerable<UserModel>> GetAllAsync();

	Task<UserModel?> GetByEmailAsync(string email);

	Task<UserModel?> GetByEmailWithRolesAsync(string email);

	Task<UserModel?> GetByIdAsync(object id);

	Task AddAsync(UserRegistrationModel model);

	Task BanUser(BanRequestModel banRequest);

	Task<bool> IsUserBanned(string userName);
}
