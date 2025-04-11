using Business.Models.Auth;

namespace Business.Interfaces;

public interface IAuthService
{
	Task<LoginSuccessResponseModel> LoginAsync(LoginRequestModel loginRequest);
}
