using Data.Entities;

namespace Data.Interfaces
{
	public interface IUnitOfWork
	{
		IGameRepository? GameRepository { get; }

		IRepository<Platform>? PlatformRepository { get; }

		IRepository<Genre>? GenreRepository { get; }

		IRepository<Publisher>? PublisherRepository { get; }

		IRepository<Order>? OrderRepository { get; }

		IRepository<OrderGame>? OrderDetailsRepository { get; }

		IRepository<Comment>? CommentRepository { get; }

		Task SaveAsync();
	}
}
