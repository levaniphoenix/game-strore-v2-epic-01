using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Data
{
	public static class DBSeeder
	{

		public static Genre[] Genres => [.. genres, .. subGenres];

		public static Platform[] Platforms => platforms;

		public static Game[] Games => games;

		public static GamePlatform[] GamePlatforms => gamePlatforms;

		public static GameGenre[] GameGenres => gameGenres;

		public static void Seed(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Platform>().HasData(platforms);
			modelBuilder.Entity<Genre>().HasData(genres);
			modelBuilder.Entity<Genre>().HasData(subGenres);
			modelBuilder.Entity<Game>().HasData(games);
			modelBuilder.Entity<GamePlatform>().HasData(gamePlatforms);
			modelBuilder.Entity<GameGenre>().HasData(gameGenres);
		}

		private static Platform[] platforms = new Platform[]
		{
			new Platform { Type = "Mobile", Id = Guid.NewGuid() },
			new Platform { Type = "Browser", Id = Guid.NewGuid() },
			new Platform { Type = "Desktop", Id = Guid.NewGuid() },
			new Platform { Type = "Console", Id = Guid.NewGuid() }
		};

		private static Genre[] genres = new Genre[]
			{
				new Genre { Name = "Strategy" , Id = Guid.NewGuid()},
				new Genre { Name = "Sports Races", Id = Guid.NewGuid() },
				new Genre { Name = "Action", Id = Guid.NewGuid() },
				new Genre { Name = "Adventure", Id = Guid.NewGuid() },
				new Genre { Name = "Skill", Id = Guid.NewGuid() }
			};

		private static Genre[] subGenres = new Genre[]
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

		private static Game[] games = new Game[]
			{
			 new Game { Name = "Test Game", Key = "test_game", Description = "This is a test game", Id = Guid.NewGuid(),} ,
			 new Game { Name = "Test Game 2", Key = "test_game_2", Description = "This is a test game 2", Id = Guid.NewGuid(),} ,
			 new Game { Name = "Test Game 3", Key = "test_game_3", Description = "This is a test game 3", Id = Guid.NewGuid(),}
			};

		private static GamePlatform[] gamePlatforms = new GamePlatform[]
			{
			new GamePlatform { GameId = games[0].Id, PlatformId = platforms[0].Id},
			new GamePlatform { GameId = games[0].Id, PlatformId = platforms[1].Id},
			new GamePlatform { GameId = games[1].Id, PlatformId = platforms[1].Id},
			new GamePlatform { GameId = games[2].Id, PlatformId = platforms[1].Id},
			};

		private static GameGenre[] gameGenres = new GameGenre[]
			{
			new GameGenre { GameId = games[0].Id, GenreId = genres[0].Id},
			new GameGenre { GameId = games[0].Id, GenreId = genres[1].Id},
			new GameGenre { GameId = games[1].Id, GenreId = genres[1].Id},
			new GameGenre { GameId = games[2].Id, GenreId = genres[2].Id},
			};
	}
}
