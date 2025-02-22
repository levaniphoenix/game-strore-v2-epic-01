using Business.Models;

namespace Business.Interfaces
{
	public interface IGameService : ICrud<GameModel>
	{
		string GenerateKey(string gameName);

		Task<GameModel?> GetByNameAsync(string gameName);

		Task<GameModel?> GetByKeyAsync(string key);

		Task<IEnumerable<GenreModel>> GetGenresByGamekey(string key);

		Task<IEnumerable<PlatformModel>> GetPlatformsByGamekey(string key);
	}
}
