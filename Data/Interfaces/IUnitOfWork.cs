using Data.Entities;
using Data.Repositories;

namespace Data.Interfaces
{
	public interface IUnitOfWork
	{
		IRepository<Game>? GameRepository {get; }

		IRepository<Platform>? PlatformRepository { get; }

		IRepository<Genre>? GenreRepository { get; }

		Task SaveAsync();
	}
}
