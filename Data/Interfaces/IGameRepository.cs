using Data.Entities;
using Common.Filters;

namespace Data.Interfaces;
public interface IGameRepository : IRepository<Game>
{
	Task<IEnumerable<Game>> GetAllWithFilterAsync(GameFilter filter);
}
