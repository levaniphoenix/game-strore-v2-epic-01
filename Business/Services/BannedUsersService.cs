using Business.Exceptions;
using Business.Interfaces;
using Business.Models;

namespace Business.Services;
public class BannedUsersService : IBannedUsersService
{
	public Dictionary<string, DateTime> BannedUsers { get; set; } = [];

	public void BanUser(BanRequestModel banRequest)
	{
		if (BannedUsers.TryGetValue(banRequest.UserName, out DateTime value) && value > DateTime.Now)
		{
			// do nothing
		}
		else
		{
			lock (BannedUsers)
			{
				BannedUsers[banRequest.UserName] = banRequest.Duration switch
				{
					"1 hour" => DateTime.Now.AddHours(1),
					"1 day" => DateTime.Now.AddDays(1),
					"1 week" => DateTime.Now.AddDays(7),
					"1 month" => DateTime.Now.AddMonths(1),
					"permanent" => DateTime.Now.AddYears(100),
					_ => throw new GameStoreValidationException("Invalid Duration"),
				};
			}
		}
	}

	public bool IsUserBanned(string userName)
	{
		return BannedUsers.ContainsKey(userName) && BannedUsers[userName] > DateTime.Now;
	}
}
