using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data.Data;
using Data.Entities;
using FluentAssertions;
using Gamestore.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using static Business.Models.GenreModel;
using static Business.Models.PlatformModel;

namespace Gamestore.Tests.ApiTests;
[TestFixture]
public class PlatformsControllerTests
{
	private readonly IMapper mapper = UnitTestHelper.CreateMapperProfile();

	[Test]
	public async Task GetShouldReturnOk()
	{
		var mockPlatformService = new Mock<IPlatformService>();
		mockPlatformService
			.Setup(s => s.GetAllAsync())
			.ReturnsAsync(mapper.Map<IEnumerable<PlatformModel>>(DBSeeder.Platforms));
		var platformsController = new PlatformsController(mockPlatformService.Object);

		var result = await platformsController.Get();

		var okResult = result.Result as OkObjectResult;
		var returnedPlatforms = (IEnumerable<PlatformDetails>) okResult?.Value;

		result.Result.Should().BeOfType<OkObjectResult>();
		returnedPlatforms.Should().NotBeNull();
		returnedPlatforms.Should().HaveCount(DBSeeder.Platforms.Length);
	}

	[Test]
	public async Task GetByIdShouldReturnOk()
	{
		var mockPlatformService = new Mock<IPlatformService>();
		mockPlatformService
			.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(
			(Guid id) =>
			{
				return mapper.Map<PlatformModel>(DBSeeder.Platforms.SingleOrDefault(g => g.Id == id));
			}
			);

		var platformController = new PlatformsController(mockPlatformService.Object);

		var id = DBSeeder.Platforms[0].Id;
		var result = await platformController.Get(id);

		var okResult = result.Result as OkObjectResult;
		var returnedPlatform = (PlatformDetails?)okResult?.Value;

		result.Result.Should().BeOfType<OkObjectResult>();
		returnedPlatform.Should().NotBeNull();
	}

	[Test]
	public async Task GetByIdShouldReturnNotFound()
	{
		var mockPlatformService = new Mock<IPlatformService>();
		mockPlatformService
			.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(
			(Guid id) =>
			{
				return mapper.Map<PlatformModel>(DBSeeder.Platforms.SingleOrDefault(g => g.Id == id));
			}
			);

		var platformController = new PlatformsController(mockPlatformService.Object);

		var id = Guid.Parse("00000000-0000-0000-0000-000000000001");
		var result = await platformController.Get(id);

		result.Result.Should().BeOfType<NotFoundObjectResult>();
	}

	[Test]
	public async Task GetGamesByPlatformShouldReturnOk()
	{
		var mockPlatformService = new Mock<IPlatformService>();
		mockPlatformService
			.Setup(s => s.GetGamesByPlatformIdAsync(It.IsAny<Guid>()))
			.ReturnsAsync(
			(Guid gameId) =>
			{
				var games = (from gamePlatform in DBSeeder.GamePlatforms
							 join platform in DBSeeder.Platforms on gamePlatform.PlatformId equals platform.Id
							 join game in DBSeeder.Games on gameId equals game.Id
							 where gamePlatform.GameId == game.Id
							 select game).ToList();
				return mapper.Map<IEnumerable<GameModel?>>(games);
			}
			);

		var platformsController = new PlatformsController(mockPlatformService.Object);

		var id = DBSeeder.Platforms[0].Id;
		var result = await platformsController.GetGamesByPlatform(id);

		var okResult = result.Result as OkObjectResult;
		var returnedGames = (IEnumerable<GameDetails?>)okResult?.Value;

		result.Result.Should().BeOfType<OkObjectResult>();
		returnedGames.Should().NotBeNull();
	}

	[Test]
	public async Task PostShouldReturnOk()
	{
		var mockPlatformService = new Mock<IPlatformService>();
		mockPlatformService.Setup(s => s.AddAsync(It.IsAny<PlatformModel>()));
		var platformsController = new PlatformsController(mockPlatformService.Object);

		var platform = new PlatformModel() { Platform = new PlatformDetails() { Type = " new type" } };

		var result = await platformsController.Post(platform);

		mockPlatformService.Verify(x => x.AddAsync(It.Is<PlatformModel>(g => g.Platform.Type == platform.Platform.Type)), Times.Once);
		result.Should().BeOfType<OkResult>();
	}

	[Test]
	public async Task PutShouldReturnOk()
	{
		var mockPlatformService = new Mock<IPlatformService>();
		mockPlatformService.Setup(s => s.UpdateAsync(It.IsAny<PlatformModel>()));
		var platformsController = new PlatformsController(mockPlatformService.Object);

		var platform = new PlatformModel() { Platform = new PlatformDetails() { Type = " new type" } };

		var result = await platformsController.Put(platform);

		mockPlatformService.Verify(x => x.UpdateAsync(It.Is<PlatformModel>(g => g.Platform.Type == platform.Platform.Type)), Times.Once);
		result.Should().BeOfType<OkResult>();
	}

	[Test]
	public async Task DeleteShouldReturnOk()
	{
		var mockPlatformService = new Mock<IPlatformService>();
		mockPlatformService.Setup(s => s.DeleteAsync(It.IsAny<PlatformModel>()));
		var platformsController = new PlatformsController(mockPlatformService.Object);

		var id = Guid.Parse("00000000-0000-0000-0000-000000000001");

		var result = await platformsController.Delete(id);

		mockPlatformService.Verify(x => x.DeleteAsync(It.Is<Guid>(g => g == id)), Times.Once);
		result.Should().BeOfType<OkResult>();
	}

}
