using Business.Models;
using Common.Filters;

namespace Business.Interfaces
{
	public interface IGameService : ICrud<GameModel>
	{
		Task<GameModel?> GetByIdAsync(object id, bool includeDeleted);

		Task<IEnumerable<GameModel>> GetAllAsync(bool includeDeleted);

		Task<PaginatedGamesModel> GetAllWithFilterAsync(GameFilter filter, bool includeDeleted);

		Task<GameModel?> GetByNameAsync(string gameName, bool includeDeleted);

		Task<GameModel?> GetByKeyAsync(string key, bool includeDeleted);

		string GenerateKey(string gameName);

		Task<IEnumerable<GenreModel>> GetGenresByGamekey(string key);

		Task<IEnumerable<PlatformModel>> GetPlatformsByGamekey(string key);

		Task<PublisherModel> GetPublisherByGamekey(string key);

		Task<int> GetTotalGamesCountAsync();

		Task DeleteByKeyAsync(string key);

		Task AddToCartAsync(string key);
	}
}
