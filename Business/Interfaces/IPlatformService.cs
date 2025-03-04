using Business.Models;

namespace Business.Interfaces
{
	public interface IPlatformService : ICrud<PlatformModel>
	{
		Task<IEnumerable<GameModel?>> GetGamesByPlatformIdAsync(Guid id);
	}
}
