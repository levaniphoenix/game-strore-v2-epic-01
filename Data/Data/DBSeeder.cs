using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Data
{
	public static class DBSeeder
	{

		public static Genre[] Genres => [.. genres, .. subGenres];

		public static Platform[] Platforms => platforms;

		public static Game[] Games => games;

		public static Publisher[] Publishers => publishers;

		public static GamePlatform[] GamePlatforms => gamePlatforms;

		public static GameGenre[] GameGenres => gameGenres;

		public static Order[] Orders => orders;

		public static OrderGame[] OrderDetails => orderGames;

		public static void Seed(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Publisher>().HasData(publishers);
			modelBuilder.Entity<Platform>().HasData(platforms);
			modelBuilder.Entity<Genre>().HasData(genres);
			modelBuilder.Entity<Genre>().HasData(subGenres);
			modelBuilder.Entity<Game>().HasData(games);
			modelBuilder.Entity<GamePlatform>().HasData(gamePlatforms);
			modelBuilder.Entity<GameGenre>().HasData(gameGenres);
			modelBuilder.Entity<Order>().HasData(orders);
			modelBuilder.Entity<OrderGame>().HasData(orderGames);
		}

		private static readonly Publisher[] publishers = new Publisher[]
		{
			new Publisher { CompanyName = "Test Publisher", Id = new Guid("407fb582-9b59-4dc9-89e7-49a8a6004e20") },
			new Publisher { CompanyName = "Test Publisher 2", Id = new Guid("4ef83756-fcfc-43d5-b3e1-b91b4a316486") },
			new Publisher { CompanyName = "Test Publisher 3", Id = new Guid("82ee6a6b-2722-410b-ac63-a2f919fed6e4") }
		};

		private static readonly Platform[] platforms = new Platform[]
		{
			new Platform { Type = "Mobile", Id = new Guid("22202dc0-8bad-4413-9945-b2d9f158f8e5") },
			new Platform { Type = "Browser", Id = new Guid("487e5c00-647f-4b1d-8572-1e73f0a839e9") },
			new Platform { Type = "Desktop", Id = new Guid("e8ba3b5e-b3d2-4b98-a525-3516f870d6ea") },
			new Platform { Type = "Console", Id = new Guid("f072a5e8-47d1-4778-a508-8350f53dc3a4") }
		};

		private static readonly Genre[] genres = new Genre[]
			{
				new Genre { Name = "Strategy" , Id = new Guid("1ffb7365-526a-41db-a795-5f9920d4d29e")},
				new Genre { Name = "Sports Races", Id = new Guid("310ffb7e-03ec-4009-837d-469664b6cbe7") },
				new Genre { Name = "Action", Id = new Guid("970af4f3-0145-4c47-a747-51188db37650") },
				new Genre { Name = "Adventure", Id = new Guid("d5bb32d9-ccdf-46b0-9859-4d9d897d39af") },
				new Genre { Name = "Skill", Id = new Guid("f202516c-7243-4a1f-82a9-95a6128b14ce") }
			};

		private static readonly Genre[] subGenres = new Genre[]
		{
				new Genre { Name = "RTS", ParentGenreId = genres[0].Id , Id = new Guid("217165cd-1739-4a1c-a437-f768ef8bb0c7")},
				new Genre { Name = "TBS", ParentGenreId = genres[0].Id, Id = new Guid("387eda52-897b-4e2f-94d7-38c5620a3513") },
				new Genre { Name = "RPG", ParentGenreId = genres[0].Id, Id = new Guid("6344f3d4-78d1-49a9-b328-17384f8c8003") },
				new Genre { Name = "Rally", ParentGenreId = genres[1].Id, Id = new Guid("67435def-e5d4-45df-85db-774d03f7c88d") },
				new Genre { Name = "Arcade", ParentGenreId = genres[1].Id, Id = new Guid("818f2abb-57ae-42d5-aab1-231a5daf5c70") },
				new Genre { Name = "Formula", ParentGenreId = genres[1].Id, Id = new Guid("93a10982-1873-4e84-ae7e-54c62cf6e2d2") },
				new Genre { Name = "Off-road", ParentGenreId = genres[1].Id, Id = new Guid("958b7723-fb54-4016-badb-86351ef95a16") },
				new Genre { Name = "FPS", ParentGenreId = genres[2].Id, Id = new Guid("b0e9e6ce-1596-4dd2-a880-0257693e8b7e") },
				new Genre { Name = "TPS", ParentGenreId = genres[2].Id, Id = new Guid("d40645c6-697a-4444-b205-206047d77b18") },
				new Genre { Name = "Puzzle", ParentGenreId = genres[3].Id, Id = new Guid("d7359da5-b867-4b1a-962c-dda749590342") },
		};

		private static readonly Game[] games = new Game[]
			{
			 new Game { Name = "Test Game", Key = "test_game", Description = "This is a test game", Id = new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"), PublisherId = publishers[0].Id , Price = 10, Discount = 1 , UnitInStock = 10 } ,
			 new Game { Name = "Test Game 2", Key = "test_game_2", Description = "This is a test game 2", Id = new Guid("2d86363e-011c-43e0-95e2-17dcd5b77149"), PublisherId = publishers[1].Id, Price = 20, Discount = 2 , UnitInStock = 20} ,
			 new Game { Name = "Test Game 3", Key = "test_game_3", Description = "This is a test game 3", Id = new Guid("90b78d48-009a-4bb3-9857-cedb9e6a6b21"), PublisherId = publishers[2].Id, Price = 30, Discount = 3 , UnitInStock = 30}
			};

		private static readonly GamePlatform[] gamePlatforms = new GamePlatform[]
			{
			new GamePlatform { GameId = games[0].Id, PlatformId = platforms[0].Id},
			new GamePlatform { GameId = games[0].Id, PlatformId = platforms[1].Id},
			new GamePlatform { GameId = games[1].Id, PlatformId = platforms[1].Id},
			new GamePlatform { GameId = games[2].Id, PlatformId = platforms[1].Id},
			};

		private static readonly GameGenre[] gameGenres = new GameGenre[]
			{
			new GameGenre { GameId = games[0].Id, GenreId = genres[0].Id},
			new GameGenre { GameId = games[0].Id, GenreId = genres[1].Id},
			new GameGenre { GameId = games[1].Id, GenreId = genres[1].Id},
			new GameGenre { GameId = games[2].Id, GenreId = genres[2].Id},
			};

		private static readonly Order[] orders = new Order[]
			{
				new Order { CustomerId = Guid.Parse("00000000-0000-0000-0000-000000000000"), Id = new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"), Date = DateTime.Now, Status = OrderStatus.Open },
			};

		private static readonly OrderGame[] orderGames = new OrderGame[]
		{
			new OrderGame { OrderId = orders[0].Id, ProductId = games[0].Id, Discount = 0, Price = 60, Quantity = 1 },
		};
	}
}
