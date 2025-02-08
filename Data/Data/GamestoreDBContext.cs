using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Data
{
	public class GamestoreDBContext : DbContext
	{
		public GamestoreDBContext(DbContextOptions<GamestoreDBContext> options) : base(options)
		{
		}

		public DbSet<Game> Games { get; set; } = default!;
		public DbSet<Genre> Genres { get; set; } = default!;
		public DbSet<Platform> Platforms { get; set; } = default!;
		public DbSet<GameGenre> GameGenres { get; set; } = default!;
		public DbSet<GamePlatform> GamePlatforms { get; set; } = default!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Game>().HasIndex(g => g.Key).IsUnique();
			modelBuilder.Entity<Game>().HasIndex(g => g.Name).IsUnique();

			modelBuilder.Entity<Genre>().HasIndex(g => g.Name).IsUnique();

			modelBuilder.Entity<Platform>().HasIndex(p => p.Type).IsUnique();

			//Game-Genre combinations are unique.
			modelBuilder.Entity<GameGenre>().HasKey(gg => new { gg.GameId, gg.GenreId });

			//Game-Platform combinations are unique.
			modelBuilder.Entity<GamePlatform>().HasKey(gp => new { gp.GameId, gp.PlatformId });

			seed(modelBuilder);
		}

		private static void seed(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Platform>().HasData(
				new Platform { Type = "Mobile" , Id = Guid.NewGuid() },
				new Platform { Type = "Browser", Id = Guid.NewGuid() },
				new Platform { Type = "Desktop", Id = Guid.NewGuid() },
				new Platform { Type = "Console", Id = Guid.NewGuid() }
				);

			var genres = new Genre[]
			{
				new Genre { Name = "Strategy" , Id = Guid.NewGuid()},
				new Genre { Name = "Sports Races", Id = Guid.NewGuid() },
				new Genre { Name = "Action", Id = Guid.NewGuid() }, 
				new Genre { Name = "Adventure", Id = Guid.NewGuid() },
				new Genre { Name = "Skill", Id = Guid.NewGuid() }
			};

			var subGenres = new Genre[]
			{
				new Genre { Name = "RTS", ParentGenreId = genres[0].Id , Id = Guid.NewGuid()},
				new Genre { Name = "TBS", ParentGenreId = genres[0].Id, Id = Guid.NewGuid() },
				new Genre { Name = "RPG", ParentGenreId = genres[0].Id, Id = Guid.NewGuid() },
				new Genre { Name = "Rally", ParentGenreId = genres[1].Id, Id = Guid.NewGuid() },
				new Genre { Name = "Arcade", ParentGenreId = genres[1].Id, Id = Guid.NewGuid() },
				new Genre { Name = "Formula", ParentGenreId = genres[1].Id, Id = Guid.NewGuid() },
				new Genre { Name = "Off-road", ParentGenreId = genres[1].Id, Id = Guid.NewGuid() },
				new Genre { Name = "FPS", ParentGenreId = genres[2].Id, Id = Guid.NewGuid() },
				new Genre { Name = "TPS", ParentGenreId = genres[2].Id, Id = Guid.NewGuid() },
				new Genre { Name = "Puzzle", ParentGenreId = genres[3].Id, Id = Guid.NewGuid() },
			};

			modelBuilder.Entity<Genre>().HasData(genres);
			modelBuilder.Entity<Genre>().HasData(subGenres);
		}
	}
}
