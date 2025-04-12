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
		public async Task GetByIDReturnsSingleValue(int i)
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
		public async Task GetByKeyReturnsSingleValue(int i)
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
		public async Task GetAllReturnsAllValues()
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
		public async Task AddAsyncAddsValueToDatabase()
		{
			// Arrange
			var unitOfWork = new UnitOfWork(context);

			var game = new Game
			{
				Id = Guid.NewGuid(),
				Name = "Test Game4",
				Key = "test_game_4",
				Description = "This is a test game",
				PublisherId = DBSeeder.Publishers[0].Id,
			};

			//act
			await unitOfWork.GameRepository.AddAsync(game);
			await unitOfWork.SaveAsync();

			//assert
			var result = await unitOfWork.GameRepository.GetByIDAsync(game.Id);

			Assert.That(result, Is.EqualTo(game).Using(new GameEqualityComparer()), message: "AddAsync method is inccorect");
		}

		[Test]
		public async Task UpdateAsyncUpdatesValueInDatabase()
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
		public async Task DeleteAsyncRemovesValueFromDatabase()
		{
			// Arrange
			var unitOfWork = new UnitOfWork(context);
			var game = DBSeeder.Games[0];
			//act
			unitOfWork.GameRepository.Delete(game);
			await unitOfWork.SaveAsync();
			//assert
			var result = await unitOfWork.GameRepository.GetByIDAsync(game.Id);
			Assert.That(result.IsDeleted, Is.True, message: "DeleteAsync method is inccorect");
		}

		[Test]
		public async Task DeleteByIdAsyncRemovesValueFromDatabase()
		{
			// Arrange
			var unitOfWork = new UnitOfWork(context);
			var game = DBSeeder.Games[0];
			//act
			await unitOfWork.GameRepository.DeleteByIdAsync(game.Id);
			await unitOfWork.SaveAsync();
			//assert
			var result = await unitOfWork.GameRepository.GetByIDAsync(game.Id);
			Assert.That(result.IsDeleted, Is.True, message: "DeleteByIdAsync method is inccorect");
		}

		[Test]
		public async Task GetAllWithIncludeReturnsAllValuesWithIncludedProperties()
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

			var expectedPublishers = from g in DBSeeder.Games
									 join p in DBSeeder.Publishers on g.PublisherId equals p.Id
									 orderby p.Id
									 select p;

			//act
			var result = await unitOfWork.GameRepository.GetAllAsync(includeProperties: "Platforms,Genres,Publisher,OrderGames");
			Assert.Multiple(() =>
			{
				//assert
				Assert.That(result.Select(i => i.Publisher).OrderBy(i => i.Id),
					Is.EqualTo(expectedPublishers).Using(new PublisherEqualityComparer()), message: "GetAllAsync does not return included entities");

				Assert.That(result.SelectMany(i => i.Platforms).OrderBy(i => i.Id),
					Is.EqualTo(expectedPlatforms).Using(new PlatformEqualityComparer()), message: "GetAllAsync does not return inclided entities");

				Assert.That(result.SelectMany(i => i.Genres).OrderBy(i => i.Id),
					Is.EqualTo(expectedGenres).Using(new GenreEqualityComparer()), message: "GetAllAsync does not return included entities");

				Assert.That(result, Is.EquivalentTo(expected).Using(new GameEqualityComparer()), message: "GetAllAsync does not work");
			});
		}

		[Test]
		public async Task AddingGameWithDuplicateNameThrowsException()
		{
			var unitOfWork = new UnitOfWork(context);
			var game = new Game { Name = "Test Game", Key = "test_game", Description = "This is a test game", Id = Guid.NewGuid(), };

			await unitOfWork.GameRepository.AddAsync(game);
			Assert.ThrowsAsync<DbUpdateException>(unitOfWork.SaveAsync);
		}

		[Test]
		public async Task AddingGameWithDuplicateKeyThrowsException()
		{
			var unitOfWork = new UnitOfWork(context);
			var game = new Game { Name = "Test Game123", Key = "test_game", Description = "This is a test game", Id = Guid.NewGuid(), };

			await unitOfWork.GameRepository.AddAsync(game);
			Assert.ThrowsAsync<DbUpdateException>(unitOfWork.SaveAsync);
		}


		[Test]
		public void UpdatingGameWithDuplicateNameThrowsException()
		{
			var unitOfWork = new UnitOfWork(context);
			var game = DBSeeder.Games[0];
			var originalName = game.Name;
			game.Name = "Test Game 2";
			unitOfWork.GameRepository.Update(game);
			Assert.ThrowsAsync<DbUpdateException>(unitOfWork.SaveAsync);
			game.Name = originalName;
		}

		[Test]
		public void UpdatingGameWithDuplicateKeyThrowsException()
		{
			var unitOfWork = new UnitOfWork(context);
			var game = DBSeeder.Games[0];
			var originalKey = game.Key;
			game.Key = "test_game_2";
			unitOfWork.GameRepository.Update(game);
			Assert.ThrowsAsync<DbUpdateException>(unitOfWork.SaveAsync);
			game.Key = originalKey;
		}
	}
}
