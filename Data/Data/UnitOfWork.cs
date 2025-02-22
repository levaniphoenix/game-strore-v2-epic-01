using Data.Entities;
using Data.Interfaces;
using Data.Repositories;

namespace Data.Data
{
	public class UnitOfWork(GamestoreDBContext context) : IUnitOfWork
	{
		private readonly GamestoreDBContext context = context;
		private IRepository<Game>? gameRepository;
		private IRepository<Platform>? platformRepository;
		private IRepository<Genre>? genreRepository;

		public IRepository<Game> GameRepository
		{
			get
			{
				gameRepository ??= new GameRepository(context);
				return gameRepository;
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
		public async Task SaveAsync()
		{
			await context.SaveChangesAsync();
		}
	}
}
