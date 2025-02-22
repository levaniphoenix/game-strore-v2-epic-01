using Business.Exceptions;
using Business.Models;
using Business.Services;
using Data.Data;
using Data.Entities;
using Data.Interfaces;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using static Business.Models.PlatformModel;

namespace Gamestore.Tests.BusinessTests
{
	[TestFixture]
	public class PlatformServiceTests
	{
		[Test]
		public async Task PlatformServiceAddAsyncAddsToDB()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(u => u.PlatformRepository!.AddAsync(It.IsAny<Platform>()));
			UnitTestHelper.SetUpMockPlatformRepository(mockUnitOfWork, DBSeeder.Platforms);
			var platformService = new PlatformService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
			await platformService.AddAsync(new PlatformModel { Platform = new PlatformDetails { Type = "Xbox" } });
			mockUnitOfWork.Verify(u => u.PlatformRepository!.AddAsync(It.IsAny<Platform>()), Times.Once);
			mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
		}

		[Test]
		public void PlatformServiceAddAsyncThrowsExceptionOnDuplicateType()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(u => u.PlatformRepository!.AddAsync(It.IsAny<Platform>()));
			UnitTestHelper.SetUpMockPlatformRepository(mockUnitOfWork, DBSeeder.Platforms);
			var platformService = new PlatformService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
			Assert.ThrowsAsync<GameStoreValidationException>(() => platformService.AddAsync(new PlatformModel { Platform = new PlatformDetails { Type = "Desktop" } }));
		}

		[Test]
		public async Task PlatformServiceDeleteAsyncDeletesFromDB()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(u => u.PlatformRepository!.DeleteByIdAsync(It.IsAny<object>()));
			UnitTestHelper.SetUpMockPlatformRepository(mockUnitOfWork, DBSeeder.Platforms);
			var platformService = new PlatformService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
			var id = DBSeeder.Platforms[0].Id;
			await platformService.DeleteAsync(id);
			mockUnitOfWork.Verify(u => u.PlatformRepository!.DeleteByIdAsync(It.IsAny<object>()), Times.Once);
			mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
		}

		[Test]
		public async Task PlatformServiceGetAllAsyncReturnsPlatforms()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			UnitTestHelper.SetUpMockPlatformRepository(mockUnitOfWork, DBSeeder.Platforms);
			var mapper = UnitTestHelper.CreateMapperProfile();
			var platformService = new PlatformService(mockUnitOfWork.Object, mapper);
			var platforms = await platformService.GetAllAsync();
			var expectedPlatforms = mapper.Map<IEnumerable<PlatformModel>>(DBSeeder.Platforms);
			platforms.Should().BeEquivalentTo(expectedPlatforms);
		}

		[Test]
		public async Task PlatformServiceGetByIdAsyncReturnsPlatform()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(m => m.PlatformRepository!.GetByIDAsync(It.IsAny<object>()))
				.ReturnsAsync((object id) => DBSeeder.Platforms.SingleOrDefault(p => p.Id == (Guid)id));

			var mapper = UnitTestHelper.CreateMapperProfile();
			var platformService = new PlatformService(mockUnitOfWork.Object, mapper);
			var platform = await platformService.GetByIdAsync(DBSeeder.Platforms[0].Id);
			var expectedPlatform = mapper.Map<PlatformModel>(DBSeeder.Platforms[0]);
			platform.Should().BeEquivalentTo(expectedPlatform);
		}

		[Test]
		public async Task PlatformServiceGetByIdAsyncReturnsNull()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(m => m.PlatformRepository!.GetByIDAsync(It.IsAny<object>()))
				.ReturnsAsync((object id) => DBSeeder.Platforms.SingleOrDefault(p => p.Id == (Guid)id));
			var mapper = UnitTestHelper.CreateMapperProfile();
			var platformService = new PlatformService(mockUnitOfWork.Object, mapper);
			var id = Guid.Parse("00000000-0000-0000-0000-000000000002");
			var platform = await platformService.GetByIdAsync(id);
			platform.Should().BeNull();
		}

		[Test]
		public async Task PlatformServiceUpdateAsyncUpdatesDB()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(u => u.PlatformRepository!.Update(It.IsAny<Platform>()));
			UnitTestHelper.SetUpMockPlatformRepository(mockUnitOfWork, DBSeeder.Platforms);
			var platformService = new PlatformService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
			await platformService.UpdateAsync(new PlatformModel { Platform = new PlatformDetails { Id = DBSeeder.Platforms[0].Id, Type = "Xbox" } });
			mockUnitOfWork.Verify(u => u.PlatformRepository!.Update(It.IsAny<Platform>()), Times.Once);
			mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
		}

		[Test]
		public void PlatformServiceUpdateAsyncThrowsExceptionOnDuplicateType()
		{
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(u => u.PlatformRepository!.Update(It.IsAny<Platform>()));
			UnitTestHelper.SetUpMockPlatformRepository(mockUnitOfWork, DBSeeder.Platforms);
			var platformService = new PlatformService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
			Assert.ThrowsAsync<GameStoreValidationException>(() => platformService.UpdateAsync(new PlatformModel { Platform = new PlatformDetails { Id = DBSeeder.Platforms[0].Id, Type = "Console" } }));
		}
	}
}
