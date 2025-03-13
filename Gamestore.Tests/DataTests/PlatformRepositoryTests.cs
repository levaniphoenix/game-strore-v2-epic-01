using Data.Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.Tests.DataTests
{
	[TestFixture]
	public class PlatformRepositoryTests
	{

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
		public async Task GetAllReturnsAllValues()
		{
			// Arrange
			var unitOfWork = new UnitOfWork(context);
			var expected = DBSeeder.Platforms;
			//act
			var result = await unitOfWork.PlatformRepository.GetAllAsync();
			//assert
			Assert.That(result, Is.EquivalentTo(expected).Using(new PlatformEqualityComparer()), message: "GetAllAsync method is inccorect");
		}

		[Test]
		public async Task GetByIDReturnsSingleValue()
		{
			// Arrange
			var unitOfWork = new UnitOfWork(context);
			var expected = DBSeeder.Platforms[0];
			//act
			var result = await unitOfWork.PlatformRepository.GetByIDAsync(expected.Id);
			//assert
			Assert.That(result, Is.EqualTo(expected).Using(new PlatformEqualityComparer()), message: "GetByIDAsync method is inccorect");
		}

		[Test]
		public async Task GetByTypeReturnsSingleValue()
		{
			// Arrange
			var unitOfWork = new UnitOfWork(context);
			var expected = DBSeeder.Platforms[0];
			//act
			var result = await unitOfWork.PlatformRepository.GetAllAsync(g => g.Type == expected.Type);
			//assert
			Assert.That(result.Count, Is.EqualTo(1));
			Assert.That(result.First, Is.EqualTo(expected).Using(new PlatformEqualityComparer()), message: "getting by the Type is inccorect");
		}

		[Test]
		public async Task AddAsyncAddsSingleValue()
		{
			// Arrange
			var unitOfWork = new UnitOfWork(context);
			var expected = new Platform { Type = "Mobile_Apple", Id = Guid.NewGuid() };
			//act
			await unitOfWork.PlatformRepository.AddAsync(expected);
			await unitOfWork.SaveAsync();
			var result = await unitOfWork.PlatformRepository.GetByIDAsync(expected.Id);
			//assert
			Assert.That(result, Is.EqualTo(expected).Using(new PlatformEqualityComparer()), message: "AddAsync method is inccorect");
		}

		[Test]
		public async Task DeleteAsyncRemovesSingleValue()
		{
			// Arrange
			var unitOfWork = new UnitOfWork(context);
			var expected = DBSeeder.Platforms[0];
			//act
			unitOfWork.PlatformRepository.Delete(expected);
			await unitOfWork.SaveAsync();
			var result = await unitOfWork.PlatformRepository.GetByIDAsync(expected.Id);
			//assert
			Assert.That(result, Is.Null, message: "DeleteAsync method is inccorect");
		}

		[Test]
		public async Task UpdateAsyncUpdatesValueInDatabase()
		{
			// Arrange
			var unitOfWork = new UnitOfWork(context);
			var platform = DBSeeder.Platforms[0];
			var expected = new Platform { Type = "Test", Id = platform.Id };
			//act
			unitOfWork.PlatformRepository.Update(expected);
			await unitOfWork.SaveAsync();
			var result = await unitOfWork.PlatformRepository.GetByIDAsync(platform.Id);
			//assert
			Assert.That(result, Is.EqualTo(expected).Using(new PlatformEqualityComparer()), message: "UpdateAsync method is inccorect");
		}

		[Test]
		public async Task DeleteByIdRemovesValueFromDataBase()
		{
			// Arrange
			var unitOfWork = new UnitOfWork(context);
			var platform = DBSeeder.Platforms[0];
			//act
			await unitOfWork.PlatformRepository.DeleteByIdAsync(platform.Id);
			await unitOfWork.SaveAsync();
			var result = await unitOfWork.PlatformRepository.GetByIDAsync(platform.Id);
			//assert
			Assert.That(result, Is.Null, message: "DeleteAsync method is inccorect");
		}

		[Test]
		public async Task AddingPlatformWithDuplicateTypeThrowsException()
		{
			// Arrange
			var unitOfWork = new UnitOfWork(context);
			var platform = new Platform { Type = "Mobile" };
			//act
			await unitOfWork.PlatformRepository.AddAsync(platform);
			//assert
			Assert.ThrowsAsync<DbUpdateException>(unitOfWork.SaveAsync);
		}

		[Test]
		public void UpdatingPlatformWithDuplicateTypeThrowsException()
		{
			// Arrange
			var unitOfWork = new UnitOfWork(context);
			var platform = DBSeeder.Platforms[0];
			var originalType = platform.Type;
			platform.Type = "Console";
			//act
			unitOfWork.PlatformRepository.Update(platform);
			//assert
			Assert.ThrowsAsync<DbUpdateException>(unitOfWork.SaveAsync);
			platform.Type = originalType;
		}
	}
}
