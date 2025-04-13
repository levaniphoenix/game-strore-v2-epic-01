using Business.Models;
using Business.Models.Auth;

namespace Business.Interfaces;

public interface IUserService : ICrud<UserModel>
{
	Task<UserModel?> GetByEmailAsync(string email);

	Task<UserModel?> GetByEmailWithRolesAsync(string email);

	Task<UserModel?> GetByIdWithRolesAsync(Guid id);

	Task AddAsync(UserRegistrationModel model);

	Task AddAsync(UserDirectAddOrUpdateModel model);

	Task BanUser(BanRequestModel banRequest);

	Task<bool> IsUserBanned(string userName);

	Task UpdateAsync(UserDirectAddOrUpdateModel model);
}
