﻿using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data.Data;
using FluentAssertions;
using Gamestore.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Gamestore.Tests.ApiTests;
[TestFixture]
public class GamesControllerTests
{
	private readonly IMapper mapper = UnitTestHelper.CreateMapperProfile();

	[Test]
	public async Task GetShouldReturnAllGames()
	{
		var mockGameService = new Mock<IGameService>();
		mockGameService
			.Setup(s => s.GetAllAsync())
			.ReturnsAsync(mapper.Map<IEnumerable<GameModel>>(DBSeeder.Games));
		var gamesController = new GamesController(mockGameService.Object);

		var result = await gamesController.Get();

		var okResult = result.Result as OkObjectResult;
		var returnedGames =(IEnumerable<GameDetails>) okResult?.Value;

		okResult.Should().NotBeNull();
		returnedGames.Should().NotBeNull();
		returnedGames.Count().Should().Be(DBSeeder.Games.Length);
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
		var gamesController = new GamesController(mockGameService.Object);

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
		var gamesController = new GamesController(mockGameService.Object);

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
		var gamesController = new GamesController(mockGameService.Object);

		var result = await gamesController.GetGenresByGamekey("test_game");

		var okResult = result.Result as OkObjectResult;
		var returnedGenres = (IEnumerable<GenreModel>) okResult?.Value;

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
		var gamesController = new GamesController(mockGameService.Object);

		var result = await gamesController.GetGenresByGamekey("test_game_Non_existent_key");

		var okResult = result.Result as OkObjectResult;
		var returnedGenres = (IEnumerable<GenreModel>)okResult?.Value;

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

		var gamesController = new GamesController(mockGameService.Object);

		var result = await gamesController.GetPlatformsByGamekey("test_game");

		var okResult = result.Result as OkObjectResult;
		var returnedGenres = (IEnumerable<PlatformModel>)okResult?.Value;

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

		var gamesController = new GamesController(mockGameService.Object);

		var result = await gamesController.GetPlatformsByGamekey("test_game_Non_existent_key");

		var okResult = result.Result as OkObjectResult;
		var returnedGenres = (IEnumerable<PlatformModel>)okResult?.Value;

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
		var gamesController = new GamesController(mockGameService.Object);

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
		var gamesController = new GamesController(mockGameService.Object);

		var result = await gamesController.GetById(Guid.Parse("00000000-0000-0000-0000-000000000001"));

		var notFoundResult = result.Result as NotFoundObjectResult;

		notFoundResult.Should().NotBeNull();
	}

	[Test]
	public async Task PostShouldReturnOk()
	{
		var mockGameService = new Mock<IGameService>();
		mockGameService.Setup(s => s.AddAsync(It.IsAny<GameModel>()));
		var gamesController = new GamesController(mockGameService.Object);

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
		var gamesController = new GamesController(mockGameService.Object);

		var game = new GameModel() { Game = new GameDetails() { Name = "a new game", Description = "game desc" } };

		var result = await gamesController.Put(game);

		mockGameService.Verify(x => x.UpdateAsync(It.Is<GameModel>(g => g.Game.Name == game.Game.Name && g.Game.Description == game.Game.Description)), Times.Once);
		result.Should().BeOfType<OkResult>();
	}

	[Test]
	public async Task DeleteShouldReturnOk()
	{
		var mockGameService = new Mock<IGameService>();
		mockGameService.Setup(s => s.DeleteAsync(It.IsAny<object>()));
		var gamesController = new GamesController(mockGameService.Object);

		var id = Guid.Parse("00000000-0000-0000-0000-000000000001");

		var result = await gamesController.Delete(id);

		mockGameService.Verify(x => x.DeleteAsync(It.Is<Guid>(g => g == id)), Times.Once);
		result.Should().BeOfType<OkResult>();
	}
}
