using System.Linq.Expressions;
using Business.Exceptions;
using Business.Models;
using Business.Services;
using Data.Data;
using Data.Entities;
using Data.Interfaces;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace Gamestore.Tests.BusinessTests
{
	[TestFixture]
	public class GameServiceTests
	{
		private readonly ILogger<GameService> logger = Mock.Of<ILogger<GameService>>();

		//weird fix to force efc to use the correct game context
		private GamestoreDBContext context;

		[SetUp]
		public void Setup()
		{
			context = new GamestoreDBContext(UnitTestHelper.GetUnitTestDbOptions());
			context.Database.OpenConnection();
			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();
		}

		[TearDown]
		public void Teardown()
		{
			context.Database.CloseConnection();
			context.Dispose();
		}

		[Test]
		public async Task GameSercviceGetAllAsyncReturnsGamesFromDB()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(m => m.GameRepository!.GetAllAsync(It.IsAny<Expression<Func<Game, bool>>?>(), It.IsAny<Func<IQueryable<Game>, IOrderedQueryable<Game>>?>(), It.IsAny<string>()))
				.ReturnsAsync(DBSeeder.Games);

			var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), logger);

			var actual = await gameService.GetAllAsync();

			actual.Should().NotBeNullOrEmpty();
		}

		[Test]
		public async Task GameSercviceAddAsyncAddsGameToDB()
		{
			var game = new GameModel() { Game = new GameDetails { Name = "a new Game", Description = "Test Game desc", }, GenreIds = [DBSeeder.Genres[0].Id], PlatformIds = [DBSeeder.Platforms[0].Id, DBSeeder.Platforms[1].Id] };

			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(m => m.GameRepository!.AddAsync(It.IsAny<Game>()));
			UnitTestHelper.SetUpMockGameRepository(mockUnitOfWork, DBSeeder.Games);
			UnitTestHelper.SetUpMockPlatformRepository(mockUnitOfWork, DBSeeder.Platforms);
			UnitTestHelper.SetUpMockGenreRepository(mockUnitOfWork, DBSeeder.Genres);

			var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), logger);

			await gameService.AddAsync(game);

			mockUnitOfWork.Verify(x => x.GameRepository!.AddAsync(It.Is<Game>(x =>
				x.Name == game.Game.Name && x.Description == game.Game.Description)), Times.Once);
			mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
		}

		[Test]
		public async Task GameServiceAddAsyncThrowsArgumentNullException()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(m => m.GameRepository!.AddAsync(It.IsAny<Game>()));

			var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), logger);

			GameModel? game = null;

			var act = async () => await gameService.AddAsync(game!);

			await act.Should().ThrowAsync<ArgumentNullException>();
		}

		[Test]
		public async Task GameServiceAddAsyncThrowsGameStoreExeptionWithDuplicateName()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(m => m.GameRepository!.AddAsync(It.IsAny<Game>()));
			UnitTestHelper.SetUpMockGameRepository(mockUnitOfWork, DBSeeder.Games);

			GameModel game = new GameModel() { Game = new GameDetails { Name = "Test Game", Description = "Test Game desc" } };

			var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), logger);

			var act = async () => await gameService.AddAsync(game);

			await act.Should().ThrowAsync<GameStoreException>().WithMessage(ErrorMessages.GameNameAlreadyExists);
		}

		[Test]
		public async Task GameServiceAddAsyncThrowsGameStoreExeptionWithDuplicateKey()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(m => m.GameRepository!.AddAsync(It.IsAny<Game>()));
			UnitTestHelper.SetUpMockGameRepository(mockUnitOfWork, DBSeeder.Games);

			GameModel game = new GameModel() { Game = new GameDetails { Name = "Test Game 10", Key = "test_game_2", Description = "Test Game desc" } };

			var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), logger);

			var act = async () => await gameService.AddAsync(game);

			await act.Should().ThrowAsync<GameStoreException>().WithMessage(ErrorMessages.GameKeyAlreadyExists);
		}

		[Test]
		public async Task GameServiceDeleteAsyncDeletesGameFromDB()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(m => m.GameRepository!.DeleteByIdAsync(It.IsAny<Guid>()));
			UnitTestHelper.SetUpMockGameRepository(mockUnitOfWork, DBSeeder.Games);
			var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), logger);
			await gameService.DeleteAsync(DBSeeder.Games[0].Id);
			mockUnitOfWork.Verify(x => x.GameRepository!.DeleteByIdAsync(DBSeeder.Games[0].Id), Times.Once);
			mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
		}

		[Test]
		public async Task GameServiceGetByKeyAsyncReturnsGameFromDB()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			UnitTestHelper.SetUpMockGameRepository(mockUnitOfWork, DBSeeder.Games);
			var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), logger);
			var actual = await gameService.GetByKeyAsync(DBSeeder.Games[0].Key);
			actual.Should().NotBeNull();
		}

		[Test]
		public async Task GameServiceGetByKeyAsyncThrowsArgumentNullException()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			UnitTestHelper.SetUpMockGameRepository(mockUnitOfWork, DBSeeder.Games);
			var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), logger);
			var act = async () => await gameService.GetByKeyAsync("");
			await act.Should().ThrowAsync<ArgumentException>();
		}

		[Test]
		public async Task GameServiceGetGenresByGameKeyReturnsGenres()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			var games = DBSeeder.Games;

			foreach (var game in games)
			{
				game.Genres = (from gameGenre in DBSeeder.GameGenres
							   join genre in DBSeeder.Genres on gameGenre.GenreId equals genre.Id
							   where gameGenre.GameId == game.Id
							   select genre).ToList();
			}

			UnitTestHelper.SetUpMockGameRepository(mockUnitOfWork, DBSeeder.Games);
			var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), logger);
			var actual = await gameService.GetGenresByGamekey(DBSeeder.Games[0].Key);
			actual.Should().NotBeNullOrEmpty();
		}

		[Test]
		public async Task GameServiceGetGenresByGameKeyThrowsArgumentNullException()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			UnitTestHelper.SetUpMockGameRepository(mockUnitOfWork, DBSeeder.Games);
			var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), logger);
			var act = async () => await gameService.GetGenresByGamekey("");
			await act.Should().ThrowAsync<ArgumentException>();
		}

		[Test]
		public async Task GameServiceGetPlatformsByGameKeyReturnsPlatforms()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			var games = DBSeeder.Games;

			foreach (var game in games)
			{
				game.Platforms = (from gamePlatform in DBSeeder.GamePlatforms
								  join platform in DBSeeder.Platforms on gamePlatform.PlatformId equals platform.Id
								  where gamePlatform.GameId == game.Id
								  select platform).ToList();
			}

			UnitTestHelper.SetUpMockGameRepository(mockUnitOfWork, DBSeeder.Games);
			var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), logger);
			var actual = await gameService.GetPlatformsByGamekey(DBSeeder.Games[0].Key);
			actual.Should().NotBeNullOrEmpty();
		}

		[Test]
		public async Task GameServiceGetPlatformsByGameKeyThrowsArgumentNullException()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			UnitTestHelper.SetUpMockGameRepository(mockUnitOfWork, DBSeeder.Games);
			var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), logger);
			var act = async () => await gameService.GetPlatformsByGamekey("");
			await act.Should().ThrowAsync<ArgumentException>();
		}

		[Test]
		public async Task GameServiceGetByIdAsyncReturnsGame()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(m => m.GameRepository!.GetByIDAsync(It.IsAny<Guid>()))
				.ReturnsAsync((Guid id) => DBSeeder.Games.SingleOrDefault(g => g.Id == id));

			var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), logger);
			var actual = await gameService.GetByIdAsync(DBSeeder.Games[0].Id);
			actual.Should().NotBeNull();
		}

		[Test]
		public async Task GameServiceGetByIdAsyncThrowsArgumentNullException()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), logger);
			var act = async () => await gameService.GetByIdAsync(null!);
			await act.Should().ThrowAsync<ArgumentNullException>();
		}

		[Test]
		public async Task GameServiceGetByIdAsyncReturnsNullForBadId()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(m => m.GameRepository!.GetByIDAsync(It.IsAny<Guid>()))
				.ReturnsAsync((Guid id) => DBSeeder.Games.SingleOrDefault(g => g.Id == id));

			var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), logger);
			var id = Guid.NewGuid();
			var actual = await gameService.GetByIdAsync(id);
			actual.Should().BeNull();
		}

		[Test]
		public async Task GameServiceGetByNameReturnsGame()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			UnitTestHelper.SetUpMockGameRepository(mockUnitOfWork, DBSeeder.Games);
			var mapper = UnitTestHelper.CreateMapperProfile();
			var gameService = new GameService(mockUnitOfWork.Object, mapper, logger);

			var actual = await gameService.GetByNameAsync(DBSeeder.Games[0].Name);
			var expected = mapper.Map<GameModel>(DBSeeder.Games[0]);

			actual.Should().BeEquivalentTo(expected);
		}

		[Test]
		public async Task GameServiceGetByNameThrowsArgumentNullException()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			UnitTestHelper.SetUpMockGameRepository(mockUnitOfWork, DBSeeder.Games);
			var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), logger);
			var act = async () => await gameService.GetByNameAsync("");
			await act.Should().ThrowAsync<ArgumentException>();
		}

		[Test]
		public async Task GameServiceUpdateAsyncUpdatesGame()
		{
			var dbGame = DBSeeder.Games[0];
			var game = new GameModel() { Game = new GameDetails { Id = dbGame.Id, Name = dbGame.Name + " ver 2", Description = "Test Game desc" } };
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(m => m.GameRepository!.Update(It.IsAny<Game>()));
			UnitTestHelper.SetUpMockGameRepository(mockUnitOfWork, DBSeeder.Games);
			UnitTestHelper.SetUpMockPlatformRepository(mockUnitOfWork, DBSeeder.Platforms);
			UnitTestHelper.SetUpMockGenreRepository(mockUnitOfWork, DBSeeder.Genres);
			var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), logger);
			await gameService.UpdateAsync(game);
			mockUnitOfWork.Verify(x => x.GameRepository!.Update(It.Is<Game>(x =>
				x.Name == game.Game.Name && x.Description == game.Game.Description)), Times.Once);
			mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
		}

		[Test]
		public async Task GameServiceUpdateAsyncThrowsArgumentNullException()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(m => m.GameRepository!.Update(It.IsAny<Game>()));
			var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), logger);
			GameModel? game = null;
			var act = async () => await gameService.UpdateAsync(game!);
			await act.Should().ThrowAsync<ArgumentNullException>();
		}

		[Test]
		public void GameServiceGenerateKeyThrowsNullArgumentException()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), logger);
			var act = () => gameService.GenerateKey(null!);
			act.Should().Throw<ArgumentNullException>();
		}

		[TestCase("Test Game", "TestGame")]
		[TestCase("Test Game 2", "TestGame2")]
		[TestCase("a new game 3", "ANewGame3")]
		public void GameServiceGenerateKeyReturnsKey(string gameName, string expectedName)
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), logger);
			var actual = gameService.GenerateKey(gameName);
			actual.Should().Be(expectedName);
		}

	}
}
