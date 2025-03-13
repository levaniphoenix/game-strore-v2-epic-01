using Business.Models;

namespace Business.Interfaces;
public interface IPublisherService : ICrud<PublisherModel>
{
	Task<IEnumerable<GameModel?>> GetGamesByPublisherIdAsync(Guid id);
}
