using Business.Models;

namespace Business.Interfaces;
public interface IPublisherService : ICrud<PublisherModel>
{
	Task<IEnumerable<GameModel?>> GetGamesByPublisherNameAsync(string name);

	Task<PublisherModel?> GetByNameAsync(string name);
}
