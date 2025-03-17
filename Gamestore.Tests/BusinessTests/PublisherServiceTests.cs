using Business.Exceptions;
using Business.Models;
using Business.Services;
using Data.Data;
using Data.Entities;
using Data.Interfaces;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Validations;
using Moq;
using static Business.Models.PublisherModel;

namespace Gamestore.Tests.BusinessTests;

[TestFixture]
public class PublisherServiceTests
{
	private readonly ILogger<PublisherService> logger = Mock.Of<ILogger<PublisherService>>();

	[Test]
	public async Task AddAsyncAddsToDB()
	{
		var mockUnitOfWork = new Mock<IUnitOfWork>();
		mockUnitOfWork.Setup(u => u.PublisherRepository!.AddAsync(It.IsAny<Publisher>()));
		UnitTestHelper.SetUpMockPublisherRepository(mockUnitOfWork, DBSeeder.Publishers);
		var publisherService = new PublisherService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), logger);

		await publisherService.AddAsync(new PublisherModel { Publisher = new PublisherDetails { CompanyName = "EA Test" } });

		mockUnitOfWork.Verify(u => u.PublisherRepository!.AddAsync(It.IsAny<Publisher>()), Times.Once);
		mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
	}

	[Test]
	public void AddAsyncThrowsExceptionOnDuplicateCompanyName()
	{
		var mockUnitOfWork = new Mock<IUnitOfWork>();
		mockUnitOfWork.Setup(u => u.PublisherRepository!.AddAsync(It.IsAny<Publisher>()));
		UnitTestHelper.SetUpMockPublisherRepository(mockUnitOfWork, DBSeeder.Publishers);
		var publisherService = new PublisherService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), logger);

		Assert.ThrowsAsync<GameStoreValidationException>(() => publisherService.AddAsync(new PublisherModel { Publisher = new PublisherDetails { CompanyName = "Test Publisher" } }));
	}

	[Test]
	public async Task DeleteAsyncDeletesFromDB()
	{
		var mockUnitOfWork = new Mock<IUnitOfWork>();
		mockUnitOfWork.Setup(u => u.PublisherRepository!.DeleteByIdAsync(It.IsAny<object>()));
		UnitTestHelper.SetUpMockPublisherRepository(mockUnitOfWork, DBSeeder.Publishers);
		var publisherService = new PublisherService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), logger);
		var id = DBSeeder.Publishers[0].Id;
		await publisherService.DeleteAsync(id);
		mockUnitOfWork.Verify(u => u.PublisherRepository!.DeleteByIdAsync(It.IsAny<object>()), Times.Once);
		mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
	}

	[Test]
	public async Task GetAllAsyncReturnsPublishers()
	{
		var mockUnitOfWork = new Mock<IUnitOfWork>();
		UnitTestHelper.SetUpMockPublisherRepository(mockUnitOfWork, DBSeeder.Publishers);
		var mapper = UnitTestHelper.CreateMapperProfile();
		var publisherService = new PublisherService(mockUnitOfWork.Object, mapper, logger);
		var publishers = await publisherService.GetAllAsync();
		var expectedPublishers = mapper.Map<IEnumerable<PublisherModel>>(DBSeeder.Publishers);
		publishers.Should().BeEquivalentTo(expectedPublishers);
	}

	[Test]
	public async Task GetByIdAsyncReturnsPublisher()
	{
		var mockUnitOfWork = new Mock<IUnitOfWork>();
		mockUnitOfWork.Setup(m => m.PublisherRepository!.GetByIDAsync(It.IsAny<object>()))
			.ReturnsAsync((object id) => DBSeeder.Publishers.SingleOrDefault(p => p.Id == (Guid)id));
		var publisherService = new PublisherService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), logger);
		var id = DBSeeder.Publishers[0].Id;
		var publisher = await publisherService.GetByIdAsync(id);
		var expectedPublisher = UnitTestHelper.CreateMapperProfile().Map<PublisherModel>(DBSeeder.Publishers[0]);
		publisher.Should().BeEquivalentTo(expectedPublisher);
	}

	[Test]
	public async Task GetByIdAsyncReturnsNull()
	{
		var mockUnitOfWork = new Mock<IUnitOfWork>();
		mockUnitOfWork.Setup(m => m.PublisherRepository!.GetByIDAsync(It.IsAny<object>()));
		UnitTestHelper.SetUpMockPublisherRepository(mockUnitOfWork, DBSeeder.Publishers);
		var publisherService = new PublisherService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), logger);
		var id = Guid.Parse("00000000-0000-0000-0000-000000000002");
		var publisher = await publisherService.GetByIdAsync(id);
		publisher.Should().BeNull();
	}

	[Test]
	public async Task UpdateAsyncUpdatesDB()
	{
		var mockUnitOfWork = new Mock<IUnitOfWork>();
		mockUnitOfWork.Setup(u => u.PublisherRepository!.Update(It.IsAny<Publisher>()));
		UnitTestHelper.SetUpMockPublisherRepository(mockUnitOfWork, DBSeeder.Publishers);
		var publisherService = new PublisherService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), logger);
		var publisher = new PublisherModel { Publisher = new PublisherDetails { CompanyName = "Test Publisher inc.", Id = DBSeeder.Platforms[0].Id } };
		await publisherService.UpdateAsync(publisher);
		mockUnitOfWork.Verify(u => u.PublisherRepository!.Update(It.IsAny<Publisher>()), Times.Once);
		mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
	}

	[Test]
	public void UpdateAsyncThrowsExceptionOnDuplicateCompanyName()
	{
		var mockUnitOfWork = new Mock<IUnitOfWork>();
		mockUnitOfWork.Setup(u => u.PublisherRepository!.Update(It.IsAny<Publisher>()));
		UnitTestHelper.SetUpMockPublisherRepository(mockUnitOfWork, DBSeeder.Publishers);
		var publisherService = new PublisherService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), logger);
		var publisher = new PublisherModel { Publisher = new PublisherDetails { CompanyName = "Test Publisher", Id = DBSeeder.Publishers[1].Id } };
		Assert.ThrowsAsync<GameStoreValidationException>(() => publisherService.UpdateAsync(publisher));
	}
}

