using Data.Entities;

namespace Data.Interfaces
{
	public interface IUnitOfWork
	{
		IRepository<Game>? GameRepository { get; }

		IRepository<Platform>? PlatformRepository { get; }

		IRepository<Genre>? GenreRepository { get; }

		IRepository<Publisher>? PublisherRepository { get; }

		Task SaveAsync();
	}
}
