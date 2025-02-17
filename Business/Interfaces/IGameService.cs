using Business.Models;

namespace Business.Interfaces
{
	public interface IGameService : ICrud<GameModel>
	{
		String GenerateKey(string gameName);

		Task<GameModel?> GetByName(string gameName);
	}
}
