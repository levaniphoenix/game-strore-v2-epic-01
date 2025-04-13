using Data.Entities;
using Data.Repositories;

namespace Data.Interfaces
{
	public interface IUnitOfWork
	{
		IGameRepository? GameRepository { get; }

		IRepository<Platform>? PlatformRepository { get; }

		IRepository<Genre>? GenreRepository { get; }

		IRepository<Publisher>? PublisherRepository { get; }

		IRepository<Order>? OrderRepository { get; }

		OrderGameRepository OrderDetailsRepository { get; }

		IRepository<Comment>? CommentRepository { get; }

		IRepository<User>? UserRepository { get; }

		IRepository<Role>? RoleRepository { get; }

		Task SaveAsync();
	}
}
