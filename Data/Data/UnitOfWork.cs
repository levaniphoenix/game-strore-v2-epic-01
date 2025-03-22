using Data.Entities;
using Data.Interfaces;
using Data.Repositories;

namespace Data.Data
{
	public class UnitOfWork(GamestoreDBContext context) : IUnitOfWork, IDisposable
	{
		private IRepository<Game>? gameRepository;
		private IRepository<Platform>? platformRepository;
		private IRepository<Genre>? genreRepository;
		private IRepository<Publisher>? publisherRepository;
		private IRepository<Order>? orderRepository;
		private IRepository<OrderGame>? orderGameRepository;
		private IRepository<Comment>? commentRepository;

		public IRepository<Comment> CommentRepository
		{
			get
			{
				commentRepository ??= new CommentRepository(context);
				return commentRepository;
			}
		}

		public IRepository<Game> GameRepository
		{
			get
			{
				gameRepository ??= new GameRepository(context);
				return gameRepository;
			}
		}
		public IRepository<Publisher> PublisherRepository
		{
			get
			{
				publisherRepository ??= new PublisherRepository(context);
				return publisherRepository;
			}
		}
		public IRepository<Platform> PlatformRepository
		{
			get
			{
				platformRepository ??= new PlatformRepository(context);
				return platformRepository;
			}
		}
		public IRepository<Genre> GenreRepository
		{
			get
			{
				genreRepository ??= new GenreRepository(context);
				return genreRepository;
			}
		}

		public IRepository<Order>? OrderRepository
		{
			get
			{
				orderRepository ??= new OrderRepository(context);
				return orderRepository;
			}
		}

		public IRepository<OrderGame>? OrderDetailsRepository
		{
			get
			{
				orderGameRepository ??= new OrderGameRepository(context);
				return orderGameRepository;
			}
		}

		public async Task SaveAsync()
		{
			await context.SaveChangesAsync();
		}

		private bool disposed;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposed && disposing)
			{
				context.Dispose();
			}
			disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
