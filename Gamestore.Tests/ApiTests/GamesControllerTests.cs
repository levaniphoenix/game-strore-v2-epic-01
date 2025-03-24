using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data.Data;
using Data.Filters;
using FluentAssertions;
using Gamestore.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using static Business.Models.GenreModel;
using static Business.Models.PlatformModel;

namespace Gamestore.Tests.ApiTests;
[TestFixture]
public class GamesControllerTests
{
	private readonly IMapper mapper = UnitTestHelper.CreateMapperProfile();

	private readonly Mock<ICommentService> mockCommentService = new Mock<ICommentService>();

	[Test]
	public async Task GetShouldReturnAllGames()
	{
		var mockGameService = new Mock<IGameService>();
		mockGameService
			.Setup(s => s.GetAllWithFilterAsync(It.IsAny<GameFilter>()))
			.ReturnsAsync(new PaginatedGamesModel() { Games = mapper.Map<IEnumerable<GameModel>>(DBSeeder.Games).Select(x => x.Game)});
		var gamesController = new GamesController(mockGameService.Object, mockCommentService.Object);

		var result = await gamesController.Get(new GameFilter());

		var okResult = result.Result as OkObjectResult;
		var returnedGames =(PaginatedGamesModel) okResult?.Value;

		okResult.Should().NotBeNull();
		returnedGames.Should().NotBeNull();
		returnedGames.Games.Count().Should().Be(DBSeeder.Games.Length);
	}

	[Test]
	public async Task GetByKeyShouldReturnOk()
	{
		var mockGameService = new Mock<IGameService>();
		mockGameService
			.Setup(s => s.GetByKeyAsync(It.IsAny<string>()))
			.ReturnsAsync((string key) => {
				return mapper.Map<GameModel?>(DBSeeder.Games.SingleOrDefault(g => g.Key == key));
			});
		var gamesController = new GamesController(mockGameService.Object, mockCommentService.Object);

		var result = await gamesController.Get("test_game");

		var okResult = result.Result as OkObjectResult;
		var returnedGames = (GameDetails?)okResult?.Value;

		okResult.Should().NotBeNull();
		returnedGames.Should().NotBeNull();
	}

	[Test]
	public async Task GetByKeyShouldReturnNotFound()
	{
		var mockGameService = new Mock<IGameService>();
		mockGameService
			.Setup(s => s.GetByKeyAsync(It.IsAny<string>()))
			.ReturnsAsync((string key) => {
				return mapper.Map<GameModel?>(DBSeeder.Games.SingleOrDefault(g => g.Key == key));
			});
		var gamesController = new GamesController(mockGameService.Object, mockCommentService.Object);

		var result = await gamesController.Get("test_game_NOT_FOUND");

		var notFoundResult = result.Result as NotFoundObjectResult;

		notFoundResult.Should().NotBeNull();
	}

	[Test]
	public async Task GetGenresByGamekeyShouldReturnOk()
	{
		var mockGameService = new Mock<IGameService>();
		mockGameService
			.Setup(s => s.GetGenresByGamekey(It.IsAny<string>()))
			.ReturnsAsync((string key) =>
			{
				var genres = (from gameGenre in DBSeeder.GameGenres
							   join genre in DBSeeder.Genres on gameGenre.GenreId equals genre.Id
							   join game in DBSeeder.Games on key equals game.Key
							   where gameGenre.GameId == game.Id
							   select genre).ToList();

				return mapper.Map<IEnumerable<GenreModel>>(genres);
			});
		var gamesController = new GamesController(mockGameService.Object, mockCommentService.Object);

		var result = await gamesController.GetGenresByGamekey("test_game");

		var okResult = result.Result as OkObjectResult;
		var returnedGenres = (IEnumerable<GenreDetails>) okResult?.Value;

		okResult.Should().NotBeNull();
		returnedGenres.Should().NotBeNull();
		returnedGenres.Count().Should().BeGreaterThan(0);
	}

	[Test]
	public async Task GetGenresByGamekeyShouldReturnOkWithEmptyList()
	{
		var mockGameService = new Mock<IGameService>();
		mockGameService
			.Setup(s => s.GetGenresByGamekey(It.IsAny<string>()))
			.ReturnsAsync((string key) =>
			{
				var genres = (from gameGenre in DBSeeder.GameGenres
							  join genre in DBSeeder.Genres on gameGenre.GenreId equals genre.Id
							  join game in DBSeeder.Games on key equals game.Key
							  where gameGenre.GameId == game.Id
							  select genre).ToList();

				return mapper.Map<IEnumerable<GenreModel>>(genres);
			});
		var gamesController = new GamesController(mockGameService.Object, mockCommentService.Object);

		var result = await gamesController.GetGenresByGamekey("test_game_Non_existent_key");

		var okResult = result.Result as OkObjectResult;
		var returnedGenres = (IEnumerable<GenreDetails>)okResult?.Value;

		okResult.Should().NotBeNull();
		returnedGenres.Should().NotBeNull();
		returnedGenres.Count().Should().Be(0);
	}

	[Test]
	public async Task GetPlatformsByGamekeyShouldReturnOk()
	{
		var mockGameService = new Mock<IGameService>();
		mockGameService
			.Setup(s => s.GetPlatformsByGamekey(It.IsAny<string>()))
			.ReturnsAsync((string key) =>
			{
				var platforms = (from gamePlatform in DBSeeder.GamePlatforms
							  join platform in DBSeeder.Platforms on gamePlatform.PlatformId equals platform.Id
							  join game in DBSeeder.Games on key equals game.Key
							  where gamePlatform.GameId == game.Id
							  select platform).ToList();

				return mapper.Map<IEnumerable<PlatformModel>>(platforms);
			});

		var gamesController = new GamesController(mockGameService.Object, mockCommentService.Object);

		var result = await gamesController.GetPlatformsByGamekey("test_game");

		var okResult = result.Result as OkObjectResult;
		var returnedGenres = (IEnumerable<PlatformDetails>)okResult?.Value;

		okResult.Should().NotBeNull();
		returnedGenres.Should().NotBeNull();
		returnedGenres.Count().Should().BeGreaterThan(0);
	}

	[Test]
	public async Task GetPlatformsByGamekeyShouldReturnOkWithEmptyList()
	{
		var mockGameService = new Mock<IGameService>();
		mockGameService
			.Setup(s => s.GetPlatformsByGamekey(It.IsAny<string>()))
			.ReturnsAsync((string key) =>
			{
				var platforms = (from gamePlatform in DBSeeder.GamePlatforms
								 join platform in DBSeeder.Platforms on gamePlatform.PlatformId equals platform.Id
								 join game in DBSeeder.Games on key equals game.Key
								 where gamePlatform.GameId == game.Id
								 select platform).ToList();

				return mapper.Map<IEnumerable<PlatformModel>>(platforms);
			});

		var gamesController = new GamesController(mockGameService.Object, mockCommentService.Object);

		var result = await gamesController.GetPlatformsByGamekey("test_game_Non_existent_key");

		var okResult = result.Result as OkObjectResult;
		var returnedGenres = (IEnumerable<PlatformDetails>)okResult?.Value;

		okResult.Should().NotBeNull();
		returnedGenres.Should().NotBeNull();
		returnedGenres.Count().Should().Be(0);
	}

	[Test]
	public async Task GetByIdShouldReturnOk()
	{
		var mockGameService = new Mock<IGameService>();
		mockGameService.Setup(s => s.GetByIdAsync(It.IsAny<object>()))
			.ReturnsAsync((object id) => { return mapper.Map<GameModel?>(DBSeeder.Games.SingleOrDefault(g => g.Id ==(Guid) id)); });
		var gamesController = new GamesController(mockGameService.Object, mockCommentService.Object);

		var result = await gamesController.GetById(DBSeeder.Games[0].Id);

		var okResult = result.Result as OkObjectResult;
		var returnedGame = (GameDetails?)okResult?.Value;

		okResult.Should().NotBeNull();
		returnedGame.Should().NotBeNull();
	}

	[Test]
	public async Task GetByIdShouldReturnNotFound()
	{
		var mockGameService = new Mock<IGameService>();
		mockGameService.Setup(s => s.GetByIdAsync(It.IsAny<object>()))
			.ReturnsAsync((object id) => { return mapper.Map<GameModel?>(DBSeeder.Games.SingleOrDefault(g => g.Id == (Guid)id)); });
		var gamesController = new GamesController(mockGameService.Object, mockCommentService.Object);

		var result = await gamesController.GetById(Guid.Parse("00000000-0000-0000-0000-000000000001"));

		var notFoundResult = result.Result as NotFoundObjectResult;

		notFoundResult.Should().NotBeNull();
	}

	[Test]
	public async Task PostShouldReturnOk()
	{
		var mockGameService = new Mock<IGameService>();
		mockGameService.Setup(s => s.AddAsync(It.IsAny<GameModel>()));
		var gamesController = new GamesController(mockGameService.Object, mockCommentService.Object);

		var game = new GameModel() { Game = new GameDetails() { Name = "a new game", Description = "game desc" } };

		var result  = await gamesController.Post(game);

		mockGameService.Verify(x => x.AddAsync(It.Is<GameModel>(g => g.Game.Name == game.Game.Name && g.Game.Description == game.Game.Description )), Times.Once);
		result.Should().BeOfType<OkResult>();
	}

	[Test]
	public async Task PutShouldReturnOk()
	{
		var mockGameService = new Mock<IGameService>();
		mockGameService.Setup(s => s.UpdateAsync(It.IsAny<GameModel>()));
		var gamesController = new GamesController(mockGameService.Object, mockCommentService.Object);

		var game = new GameModel() { Game = new GameDetails() { Name = "a new game", Description = "game desc" } };

		var result = await gamesController.Put(game);

		mockGameService.Verify(x => x.UpdateAsync(It.Is<GameModel>(g => g.Game.Name == game.Game.Name && g.Game.Description == game.Game.Description)), Times.Once);
		result.Should().BeOfType<OkResult>();
	}

	[Test]
	public async Task DeleteShouldReturnOk()
	{
		var mockGameService = new Mock<IGameService>();
		mockGameService.Setup(s => s.DeleteByKeyAsync(It.IsAny<string>()));
		var gamesController = new GamesController(mockGameService.Object, mockCommentService.Object);

		var game = DBSeeder.Games[0];

		var result = await gamesController.Delete(game.Key);

		mockGameService.Verify(x => x.DeleteByKeyAsync(It.Is<string>(g => g.Equals(game.Key))), Times.Once);
		result.Should().BeOfType<OkResult>();
	}
}
