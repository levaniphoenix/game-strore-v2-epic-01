using Data.Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.Tests
{
	internal static class UnitTestHelper
	{		
		public static DbContextOptions<GamestoreDBContext> GetUnitTestDbOptions()
		{
			var options = new DbContextOptionsBuilder<GamestoreDBContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString())
				.Options;

			using (var context = new GamestoreDBContext(options))
			{
				SeedData(context);
			}

			return options;
		}

		public static void SeedData(GamestoreDBContext context)
		{
			context.Platforms.AddRange(platforms);
			context.Genres.AddRange(genres);
			context.Genres.AddRange(subGenres);
			context.Games.AddRange(games);
			context.SaveChanges();
		}

		public static List<Platform> platforms = new List<Platform>()
			{
				new Platform { Type = "Mobile" , Id = Guid.NewGuid() },
				new Platform { Type = "Browser", Id = Guid.NewGuid() },
				new Platform { Type = "Desktop", Id = Guid.NewGuid() },
				new Platform { Type = "Console", Id = Guid.NewGuid() }
			};

		public static List<Genre> genres = new List<Genre>()
			{
				new Genre { Name = "Strategy" , Id = Guid.NewGuid()},
				new Genre { Name = "Action", Id = Guid.NewGuid() },
			};

		public static List<Genre> subGenres = new List<Genre>()
			{
				new Genre { Name = "RTS" , Parent = genres[0], Id = Guid.NewGuid()},
				new Genre { Name = "TBS", Parent = genres[0], Id = Guid.NewGuid() },
				new Genre { Name = "RPG", Parent = genres[0], Id = Guid.NewGuid() },

				new Genre { Name = "Shooter", Parent = genres[1], Id = Guid.NewGuid() },
			};

		public static List<Game> games = new List<Game>()
			{
			 new Game { Name = "Test Game", Key = "test_game", Description = "This is a test game", Id = Guid.NewGuid(), Platforms = platforms, Genres = [subGenres[0]] } ,
			 new Game { Name = "Test Game 2", Key = "test_game_2", Description = "This is a test game 2", Id = Guid.NewGuid(), Platforms = [platforms[0], platforms[1]] } ,
			 new Game { Name = "Test Game 3", Key = "test_game_3", Description = "This is a test game 3", Id = Guid.NewGuid(), Platforms = [platforms[1], platforms[2]] } 
			};
	}
}