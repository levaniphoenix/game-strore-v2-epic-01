using Data.Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.Tests.DataTests
{
	[TestFixture]
	public class GameRepositoryTests
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

		[TestCase(0)]
		[TestCase(1)]
		[TestCase(2)]
		public async Task GameRepositoryGetByIDReturnsSingleValue(int i)
		{
			// Arrange
			var unitOfWork = new UnitOfWork(context);

			var expected = DBSeeder.Games[i];

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
			var unitOfWork = new UnitOfWork(context);

			var expected = DBSeeder.Games[i];

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
			var unitOfWork = new UnitOfWork(context);
			var expected = DBSeeder.Games;
			//act
			var result = await unitOfWork.GameRepository.GetAllAsync();
			//assert
			Assert.That(result, Is.EquivalentTo(expected).Using(new GameEqualityComparer()), message: "GetAllAsync method is inccorect");
		}


		[Test]
		public async Task GameRepositoryAddAsyncAddsValueToDatabase()
		{
			// Arrange
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
			var unitOfWork = new UnitOfWork(context);
			var game = DBSeeder.Games[0];
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
			var unitOfWork = new UnitOfWork(context);
			var game = DBSeeder.Games[0];
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
			var unitOfWork = new UnitOfWork(context);
			var game = DBSeeder.Games[0];
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
			var unitOfWork = new UnitOfWork(context);
			var expected = DBSeeder.Games;
			
			var expectedGenres = from gg in DBSeeder.GameGenres
								 join genre in DBSeeder.Genres on gg.GenreId equals genre.Id
								 join game in DBSeeder.Games on gg.GameId equals game.Id
								 orderby genre.Id
								 select genre;

			var expectedPlatforms = from gp in DBSeeder.GamePlatforms
								 join p in DBSeeder.Platforms on gp.PlatformId equals p.Id
								 join g in DBSeeder.Games on gp.GameId equals g.Id
								 orderby p.Id
								 select p;

			//act
			var result = await unitOfWork.GameRepository.GetAllAsync(includeProperties: "Platforms,Genres");
			//assert

			Assert.That(result.SelectMany(i => i.Platforms).OrderBy(i => i.Id),
				Is.EqualTo(expectedPlatforms).Using(new PlatformEqualityComparer()), message: "GetAllAsync does not return inclided entities");

			Assert.That(result.SelectMany(i => i.Genres).OrderBy(i => i.Id),
				Is.EqualTo(expectedGenres).Using(new GenreEqualityComparer()), message: "GetAllAsync does not return included entities");

			Assert.That(result, Is.EquivalentTo(expected).Using(new GameEqualityComparer()), message: "GetAllAsync does not work");
		}

	}
}
