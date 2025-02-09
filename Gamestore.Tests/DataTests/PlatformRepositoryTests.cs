using Data.Data;
using Data.Entities;

namespace Gamestore.Tests.DataTests
{
	[TestFixture]
	public class PlatformRepositoryTests
	{

		[Test]
		public async Task PlatformRepositoryGetAllReturnsAllValues()
		{
			// Arrange
			using var context = new GamestoreDBContext(UnitTestHelper.GetUnitTestDbOptions());
			var unitOfWork = new UnitOfWork(context);
			var expected = UnitTestHelper.platforms;
			//act
			var result = await unitOfWork.PlatformRepository.GetAllAsync();
			//assert
			Assert.That(result, Is.EquivalentTo(expected).Using(new PlatformEqualityComparer()), message: "GetAllAsync method is inccorect");
		}

		[Test]
		public async Task PlatformRepositoryGetByIDReturnsSingleValue()
		{
			// Arrange
			using var context = new GamestoreDBContext(UnitTestHelper.GetUnitTestDbOptions());
			var unitOfWork = new UnitOfWork(context);
			var expected = UnitTestHelper.platforms[0];
			//act
			var result = await unitOfWork.PlatformRepository.GetByIDAsync(expected.Id);
			//assert
			Assert.That(result, Is.EqualTo(expected).Using(new PlatformEqualityComparer()), message: "GetByIDAsync method is inccorect");
		}

		[Test]
		public async Task PlatformRepositoryGetByTypeReturnsSingleValue()
		{
			// Arrange
			using var context = new GamestoreDBContext(UnitTestHelper.GetUnitTestDbOptions());
			var unitOfWork = new UnitOfWork(context);
			var expected = UnitTestHelper.platforms[0];
			//act
			var result = await unitOfWork.PlatformRepository.GetAllAsync(g => g.Type == expected.Type);
			//assert
			Assert.That(result.Count, Is.EqualTo(1));
			Assert.That(result.First, Is.EqualTo(expected).Using(new PlatformEqualityComparer()), message: "getting by the Type is inccorect");
		}

		[Test]
		public async Task PlatformRepositoryAddAsyncAddsSingleValue()
		{
			// Arrange
			using var context = new GamestoreDBContext(UnitTestHelper.GetUnitTestDbOptions());
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
		public async Task PlatformRepositoryDeleteAsyncRemovesSingleValue()
		{
			// Arrange
			using var context = new GamestoreDBContext(UnitTestHelper.GetUnitTestDbOptions());
			var unitOfWork = new UnitOfWork(context);
			var expected = UnitTestHelper.platforms[0];
			//act
			unitOfWork.PlatformRepository.Delete(expected);
			await unitOfWork.SaveAsync();
			var result = await unitOfWork.PlatformRepository.GetByIDAsync(expected.Id);
			//assert
			Assert.That(result, Is.Null, message: "DeleteAsync method is inccorect");
		}

		[Test]
		public async Task PlatformRepositoryUpdateAsyncUpdatesValueInDatabase()
		{
			// Arrange
			using var context = new GamestoreDBContext(UnitTestHelper.GetUnitTestDbOptions());
			var unitOfWork = new UnitOfWork(context);
			var platform = UnitTestHelper.platforms[0];
			var expected = new Platform { Type = "Test", Id = platform.Id };
			//act
			unitOfWork.PlatformRepository.Update(expected);
			await unitOfWork.SaveAsync();
			var result = await unitOfWork.PlatformRepository.GetByIDAsync(platform.Id);
			//assert
			Assert.That(result, Is.EqualTo(expected).Using(new PlatformEqualityComparer()), message: "UpdateAsync method is inccorect");
		}

		[Test]
		public async Task PlatformRepositoryDeleteByIdRemovesValueFromDataBase()
		{
			// Arrange
			using var context = new GamestoreDBContext(UnitTestHelper.GetUnitTestDbOptions());
			var unitOfWork = new UnitOfWork(context);
			var platform = UnitTestHelper.platforms[0];
			//act
			await unitOfWork.PlatformRepository.DeleteByIdAsync(platform.Id);
			await unitOfWork.SaveAsync();
			var result = await unitOfWork.PlatformRepository.GetByIDAsync(platform.Id);
			//assert
			Assert.That(result, Is.Null, message: "DeleteAsync method is inccorect");
		}
	}
}
