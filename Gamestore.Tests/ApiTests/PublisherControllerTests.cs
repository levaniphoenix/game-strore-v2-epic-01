using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data.Data;
using FluentAssertions;
using Gamestore.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using static Business.Models.PublisherModel;

namespace Gamestore.Tests.ApiTests;

[TestFixture]
public class PublisherControllerTests
{
	private readonly IMapper mapper = UnitTestHelper.CreateMapperProfile();

	[Test]
	public async Task GetShouldReturnOk()
	{
		var mockPublisherService = new Mock<IPublisherService>();
		mockPublisherService
			.Setup(s => s.GetAllAsync())
			.ReturnsAsync(mapper.Map<IEnumerable<PublisherModel>>(DBSeeder.Publishers));
		var publisherController = new PublishersController(mockPublisherService.Object);
		var result = await publisherController.Get();
		var okResult = result.Result as OkObjectResult;
		var returnedPublishers = (IEnumerable<PublisherDetails>)okResult?.Value;
		result.Result.Should().BeOfType<OkObjectResult>();
		returnedPublishers.Should().NotBeNull();
		returnedPublishers.Should().HaveCount(DBSeeder.Publishers.Length);
	}

	[Test]
	public async Task GetByNameShouldRetunOk()
	{
		var mockPublisherService = new Mock<IPublisherService>();
		mockPublisherService
			.Setup(s => s.GetByNameAsync(It.IsAny<string>()))
			.ReturnsAsync(
			(string name) =>
			{
				return mapper.Map<PublisherModel>(DBSeeder.Publishers.SingleOrDefault(p => p.CompanyName == name));
			}
			);
		var publisherController = new PublishersController(mockPublisherService.Object);
		var name = DBSeeder.Publishers[0].CompanyName;
		var result = await publisherController.Get(name);
		var okResult = result.Result as OkObjectResult;
		var returnedPublisher = (PublisherDetails?)okResult?.Value;
		result.Result.Should().BeOfType<OkObjectResult>();
		returnedPublisher.Should().NotBeNull();
	}

	[Test]
	public async Task GetByNameShouldReturnNotFound()
	{
		var mockPublisherService = new Mock<IPublisherService>();
		mockPublisherService
			.Setup(s => s.GetByNameAsync(It.IsAny<string>()))
			.ReturnsAsync((PublisherModel?)null);
		var publisherController = new PublishersController(mockPublisherService.Object);
		var name = "NonExistentPublisher";
		var result = await publisherController.Get(name);
		result.Result.Should().BeOfType<NotFoundObjectResult>();
	}

	[Test]
	public async Task GetGamesByPublisherShouldReturnOk()
	{
		var mockPublisherService = new Mock<IPublisherService>();
		mockPublisherService
			.Setup(s => s.GetGamesByPublisherNameAsync(It.IsAny<string>()))
			.ReturnsAsync(
			(string name) =>
			{
				var publisher = DBSeeder.Publishers.SingleOrDefault(p => p.CompanyName == name);
				return mapper.Map<IEnumerable<GameModel?>>(DBSeeder.Games.Where(g => g.PublisherId == publisher.Id).ToList());
			}
			);
		var publisherController = new PublishersController(mockPublisherService.Object);
		
		var name = DBSeeder.Publishers[0].CompanyName;
		
		var result = await publisherController.GetGamesByPublisher(name);
		var okResult = result.Result as OkObjectResult;
		var returnedGames = (IEnumerable<GameDetails>)okResult?.Value;
		result.Result.Should().BeOfType<OkObjectResult>();
		returnedGames.Should().NotBeNull();
		returnedGames.Should().HaveCount(DBSeeder.Games.Count(g => g.PublisherId == DBSeeder.Publishers[0].Id));
	}

	[Test]
	public async Task PostShouldReturnOk()
	{
		var mockPublisherService = new Mock<IPublisherService>();
		var publisher = new PublisherModel
		{
			Publisher = new PublisherDetails
			{
				CompanyName = "NewPublisher",
			}
		};
		mockPublisherService
			.Setup(s => s.AddAsync(It.IsAny<PublisherModel>()))
			.Returns(Task.CompletedTask);
		var publisherController = new PublishersController(mockPublisherService.Object);
		var result = await publisherController.Post(publisher);
		result.Should().BeOfType<OkResult>();
	}

	[Test]
	public async Task PutShouldReturnOk()
	{
		var mockPublisherService = new Mock<IPublisherService>();
		var publisher = new PublisherModel
		{
			Publisher = new PublisherDetails
			{
				CompanyName = "NewPublisher",
			}
		};
		mockPublisherService
			.Setup(s => s.UpdateAsync(It.IsAny<PublisherModel>()))
			.Returns(Task.CompletedTask);
		var publisherController = new PublishersController(mockPublisherService.Object);
		var result = await publisherController.Put(publisher);
		result.Should().BeOfType<OkResult>();
	}

	[Test]
	public async Task DeleteShouldReturnOk()
	{
		var mockPublisherService = new Mock<IPublisherService>();
		mockPublisherService
			.Setup(s => s.DeleteAsync(It.IsAny<Guid>()))
			.Returns(Task.CompletedTask);
		var publisherController = new PublishersController(mockPublisherService.Object);
		
		var id = Guid.Parse("00000000-0000-0000-0000-000000000001");
		
		var result = await publisherController.Delete(id);
		mockPublisherService.Verify(x => x.DeleteAsync(It.Is<Guid>(g => g == id)), Times.Once);
		result.Should().BeOfType<OkResult>();
	}
}
