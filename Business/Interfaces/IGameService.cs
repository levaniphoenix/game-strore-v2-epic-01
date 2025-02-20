using Business.Models;

namespace Business.Interfaces
{
	public interface IGameService : ICrud<GameModel>
	{
		String GenerateKey(string gameName);

		Task<GameModel?> GetByName(string gameName);

		Task<GameModel?> GetAllByKeyAsync(string key);

		Task<IEnumerable<GenreModel>> GetGenresByGamekey(string key);

		Task<IEnumerable<PlatformModel>> GetPlatformsByGamekey(string key);
	}
}
