﻿using Common.Helpers;
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

		public static Comment[] Comments => comments;

		public static Comment[] Replies => replies;

		public static Comment[] Tier3Replies => tier3replies;

		public static Role[] Roles => roles;

		public static User[] Users => users;

		public static UserRole[] UserRoles => userRoles;

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
			modelBuilder.Entity<Comment>().HasData(comments);
			modelBuilder.Entity<Comment>().HasData(replies);
			modelBuilder.Entity<Comment>().HasData(tier3replies);
			modelBuilder.Entity<Role>().HasData(roles);
			modelBuilder.Entity<User>().HasData(users);
			modelBuilder.Entity<UserRole>().HasData(userRoles);
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
			 new Game { Name = "Test Game", Key = "test_game", Description = "This is a test game", Id = new Guid("2c779a02-8e67-49e1-919b-03dd6d5f2206"), PublisherId = publishers[0].Id , Price = 10, Discount = 1 , UnitInStock = 10 , PublishDate = new DateTime(2025, 4, 11, 23, 5, 42, 807, DateTimeKind.Local).AddTicks(36)} ,
			 new Game { Name = "Test Game 2", Key = "test_game_2", Description = "This is a test game 2", Id = new Guid("2d86363e-011c-43e0-95e2-17dcd5b77149"), PublisherId = publishers[1].Id, Price = 20, Discount = 2 , UnitInStock = 20, PublishDate = new DateTime(2025, 2, 23, 0, 0, 0, DateTimeKind.Utc)} ,
			 new Game { Name = "Test Game 3", Key = "test_game_3", Description = "This is a test game 3", Id = new Guid("90b78d48-009a-4bb3-9857-cedb9e6a6b21"), PublisherId = publishers[2].Id, Price = 30, Discount = 3 , UnitInStock = 30, PublishDate = new DateTime(2024, 11, 2, 0, 0, 0, DateTimeKind.Utc) }
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
				new Order { CustomerId = Guid.Parse("00000000-0000-0000-0000-000000000000"), Id = new Guid("89866a29-ebf5-46b0-9e82-91ef26ec0447"), Date = new DateTime(2025, 4, 11, 23, 5, 42, 808, DateTimeKind.Local).AddTicks(1455), Status = OrderStatus.Open },
			};

		private static readonly OrderGame[] orderGames = new OrderGame[]
		{
			new OrderGame { OrderId = orders[0].Id, ProductId = games[0].Id, Discount = 0, Price = 60, Quantity = 1 },
		};

		private static readonly Comment[] comments = new Comment[]
		{
			new Comment { GameId = games[0].Id, Id = new Guid("bce96b76-0624-4248-9b34-d30655495c2d"), Body = "This is a test comment", Name = "Paul" },
			new Comment { GameId = games[0].Id, Id = new Guid("525d6ae7-9e7f-4514-930f-c62428c41948"), Body = "This is a test comment 2", Name = "John"},
		};

		private static readonly Comment[] replies = new Comment[]
		{
			new Comment { GameId = games[0].Id, Id = new Guid("96d2c845-1144-4a6b-a8cb-abced83a8b2c"), Body = "This is a test reply to test comment", Name = "Paul", ParentId = comments[0].Id },
			new Comment { GameId = games[0].Id, Id = new Guid("66d313b8-da86-4444-aae6-98f3737624eb"), Body = "This is a test reply to comment 2", Name = "John", ParentId = comments[1].Id },
		};

		private static readonly Comment[] tier3replies = new Comment[]
		{
			new Comment { GameId = games[0].Id, Id = new Guid("0db6fa93-f8a1-43f5-a57c-4405bce7aa63"), Body = "This is a tier 3 reply to test comment", Name = "Paul", ParentId = replies[0].Id },
		};

		private static readonly Role[] roles = new Role[]
		{
			new Role { Id = new Guid("64614001-fec4-4889-928e-61b4aab78665"), Name = "Admin", Description = "Can manage users and roles. Can see deleted games. Can manage comments for the deleted game. Can edit a deleted game." },
			new Role { Id = new Guid("8e316382-2646-4484-a79b-3a78a41131d2"), Name = "Manager", Description = "Can manage business entities: games, genres, publishers, platforms, etc. Can edit orders. Can view orders history. Can’t edit orders from history. Can change the status of an order from paid to shipped. Can't edit a deleted game." },
			new Role { Id = new Guid("7a24a152-00a7-42fb-ad05-ebb5b71ef264"), Name = "Moderator", Description = "Can manage game comments. Can ban users from commenting." },
			new Role { Id = new Guid("c54327ae-8a79-4dd0-9670-f70786ebd7f6"), Name = "User", Description = "Can’t see deleted games. Can’t buy a deleted game. Can see the games in stock. Can comment game." },
			new Role { Id = new Guid("2d32286b-8ae7-4579-92e8-0a8afa70d6ec"), Name = "Guest", Description = "Has read-only access." }
		};

		private static readonly User[] users = new User[]
		{
			new User { Id = new Guid("fc329ddf-39cd-4478-abe5-85663ca2659d"), Email = "admin", FirstName = "admin", LastName = "admin", PasswordHash = "plPkIXrCzVHytBtFEPhraFJrrik7Z5j1XfNlIB6532A=-JxvDJ4ckFVbKUHJKX5HR+g==", IsBanned = false },
		};

		private static readonly UserRole[] userRoles = new UserRole[]
		{
			new UserRole { UserId = users[0].Id, RoleId = roles[0].Id },
			new UserRole { UserId = users[0].Id, RoleId = roles[1].Id },
		};
	}
}
