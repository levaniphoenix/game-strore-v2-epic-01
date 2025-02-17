using Business.Models;

namespace Business.Interfaces
{
	public interface IGameService : ICrud<GameModel>
	{
		String GenerateKey(String gameName);
	}
}
