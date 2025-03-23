using Business.Models;

namespace Business.Interfaces;

public interface IBannedUsersService
{
	void BanUser(BanRequestModel banRequest);

	bool IsUserBanned(string userName);
}
