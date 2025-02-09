using Data.Entities;
using Data.Interfaces;
using Data.Repositories;

namespace Data.Data
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly GamestoreDBContext context;
		private IRepository<Game>? gameRepository;
		private IRepository<Platform>? platformRepository;
		private IRepository<Genre>? genreRepository;

		public UnitOfWork(GamestoreDBContext context)
		{
			this.context = context;
		}

		public IRepository<Game> GameRepository
		{
			get
			{
				if (this.gameRepository == null)
				{
					this.gameRepository = new GenericRepository<Game>(context);
				}
				return gameRepository;
			}
		}
		public IRepository<Platform> PlatformRepository
		{
			get
			{
				if (this.platformRepository == null)
				{
					this.platformRepository = new GenericRepository<Platform>(context);
				}
				return platformRepository;
			}
		}
		public IRepository<Genre> GenreRepository
		{
			get
			{
				if (this.genreRepository == null)
				{
					this.genreRepository = new GenericRepository<Genre>(context);
				}
				return genreRepository;
			}
		}
		public async Task SaveAsync()
		{
			await context.SaveChangesAsync();
		}
	}
}
