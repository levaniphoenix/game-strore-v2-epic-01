using Data.Data;
using Data.Entities;

namespace Gamestore.Tests.DataTests
{
	[TestFixture]
	public class GameRepositoryTests
	{
		[TestCase(0)]
		[TestCase(1)]
		[TestCase(2)]
		public async Task GameRepositoryGetByIDReturnsSingleValue(int i)
		{
			// Arrange
			using var context = new GamestoreDBContext(UnitTestHelper.GetUnitTestDbOptions());
			var unitOfWork = new UnitOfWork(context);

			var expected = UnitTestHelper.games[i];

			//act
			var result = await unitOfWork.GameRepository.GetByIDAsync(expected.Id);

			//assert
			Assert.That(result, Is.EqualTo(expected).Using(new GameEqualityComparer()), message: "GetByIDAsync method is inccorect");
		}

		[TestCase(0)]
		[TestCase(1)]
		[TestCase(2)]
		public async Task GameRepositoryGetByKeyReturnsSingleValue(int i)
		{
			// Arrange
			using var context = new GamestoreDBContext(UnitTestHelper.GetUnitTestDbOptions());
			var unitOfWork = new UnitOfWork(context);

			var expected = UnitTestHelper.games[i];

			//act
			var result = await unitOfWork.GameRepository.GetAllAsync(g => g.Key == expected.Key);

			//assert
			Assert.That(result.Count, Is.EqualTo(1));
			Assert.That(result.First, Is.EqualTo(expected).Using(new GameEqualityComparer()), message: "getting by the Key is inccorect");
		}

		[Test]
		public async Task GameRepositoryGetAllReturnsAllValues()
		{
			// Arrange
			using var context = new GamestoreDBContext(UnitTestHelper.GetUnitTestDbOptions());
			var unitOfWork = new UnitOfWork(context);
			var expected = UnitTestHelper.games;
			//act
			var result = await unitOfWork.GameRepository.GetAllAsync();
			//assert
			Assert.That(result, Is.EquivalentTo(expected).Using(new GameEqualityComparer()), message: "GetAllAsync method is inccorect");
		}


		[Test]
		public async Task GameRepositoryAddAsyncAddsValueToDatabase()
		{
			// Arrange
			using var context = new GamestoreDBContext(UnitTestHelper.GetUnitTestDbOptions());
			var unitOfWork = new UnitOfWork(context);

			var game = new Game
			{
				Id = Guid.NewGuid(),
				Name = "Test Game4",
				Key = "test_game_4",
				Description = "This is a test game"
			};

			//act
			await unitOfWork.GameRepository.AddAsync(game);
			await unitOfWork.SaveAsync();

			//assert
			var result = await unitOfWork.GameRepository.GetByIDAsync(game.Id);

			Assert.That(result, Is.EqualTo(game).Using(new GameEqualityComparer()), message: "AddAsync method is inccorect");
		}

		[Test]
		public async Task GameRepositoryUpdateAsyncUpdatesValueInDatabase()
		{
			// Arrange
			using var context = new GamestoreDBContext(UnitTestHelper.GetUnitTestDbOptions());
			var unitOfWork = new UnitOfWork(context);
			var game = UnitTestHelper.games[0];
			game.Name = "Updated Game";
			//act
			unitOfWork.GameRepository.Update(game);
			await unitOfWork.SaveAsync();
			//assert
			var result = await unitOfWork.GameRepository.GetByIDAsync(game.Id);
			Assert.That(result, Is.EqualTo(game).Using(new GameEqualityComparer()), message: "UpdateAsync method is inccorect");
		}

		[Test]
		public async Task GameRepositoryDeleteAsyncRemovesValueFromDatabase()
		{
			// Arrange
			using var context = new GamestoreDBContext(UnitTestHelper.GetUnitTestDbOptions());
			var unitOfWork = new UnitOfWork(context);
			var game = UnitTestHelper.games[0];
			//act
			unitOfWork.GameRepository.Delete(game);
			await unitOfWork.SaveAsync();
			//assert
			var result = await unitOfWork.GameRepository.GetByIDAsync(game.Id);
			Assert.That(result, Is.Null, message: "DeleteAsync method is inccorect");
		}

		[Test]
		public async Task GameRepositoryDeleteByIdAsyncRemovesValueFromDatabase()
		{
			// Arrange
			using var context = new GamestoreDBContext(UnitTestHelper.GetUnitTestDbOptions());
			var unitOfWork = new UnitOfWork(context);
			var game = UnitTestHelper.games[0];
			//act
			await unitOfWork.GameRepository.DeleteByIdAsync(game.Id);
			await unitOfWork.SaveAsync();
			//assert
			var result = await unitOfWork.GameRepository.GetByIDAsync(game.Id);
			Assert.That(result, Is.Null, message: "DeleteByIdAsync method is inccorect");
		}

		[Test]
		public async Task GameRepositoryGetAllWithIncludeReturnsAllValuesWithIncludedProperties()
		{
			// Arrange
			using var context = new GamestoreDBContext(UnitTestHelper.GetUnitTestDbOptions());
			var unitOfWork = new UnitOfWork(context);
			var expected = UnitTestHelper.games;

			var expectedPlatforms = expected.SelectMany(i => i.Platforms).OrderBy(i => i.Id);
			var expectedGenres = expected.SelectMany(i => i.Genres).OrderBy(i => i.Id);
			//act
			var result = await unitOfWork.GameRepository.GetAllAsync(includeProperties: "Platforms,Genres");
			//assert

			Assert.That(result.SelectMany(i => i.Platforms).OrderBy(i => i.Id),
				Is.EqualTo(expectedPlatforms).Using(new PlatformEqualityComparer()), message: "GetAllAsync does not return inclided entities");

			Assert.That(result.SelectMany(i => i.Genres).OrderBy(i => i.Id),
				Is.EqualTo(expectedGenres).Using(new GenreEqualityComparer()), message: "GetAllAsync does not return inclided entities");

			Assert.That(result, Is.EquivalentTo(expected).Using(new GameEqualityComparer()), message: "GetAllAsync does not work");
		}

	}
}
