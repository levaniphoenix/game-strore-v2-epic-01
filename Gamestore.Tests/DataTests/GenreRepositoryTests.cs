using Data.Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.Tests.DataTests
{
	[TestFixture]
	public class GenreRepositoryTests
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
		public async Task GenreRepositoryGetAllReturnsAllValues()
		{
			// Arrange
			var unitOfWork = new UnitOfWork(context);
			var expected = DBSeeder.Genres;
			//act
			var result = await unitOfWork.GenreRepository.GetAllAsync();
			//assert
			Assert.That(result, Is.EquivalentTo(expected).Using(new GenreEqualityComparer()), message: "GetAllAsync method is inccorect");
		}

		[TestCase(0)]
		[TestCase(1)]
		public async Task GenreRepositoryGetByIDReturnsSingleValue(int i)
		{
			// Arrange
			var unitOfWork = new UnitOfWork(context);
			var expected = DBSeeder.Genres[i];
			//act
			var result = await unitOfWork.GenreRepository.GetByIDAsync(expected.Id);
			//assert
			Assert.That(result, Is.EqualTo(expected).Using(new GenreEqualityComparer()), message: "GetByIDAsync method is inccorect");
		}

		[Test]
		public async Task GenreRepositoryGetAllByNameReturnsSingleValue()
		{
			// Arrange
			var unitOfWork = new UnitOfWork(context);
			var expected = DBSeeder.Genres[0];
			//act
			var result = await unitOfWork.GenreRepository.GetAllAsync(g => g.Name == expected.Name);
			//assert
			Assert.That(result.Count, Is.EqualTo(1));
			Assert.That(result.First, Is.EqualTo(expected).Using(new GenreEqualityComparer()), message: "getting by the Name is inccorect");
		}

		[Test]
		public async Task GenreRepositoryAddAsyncAddsValueToDatabase()
		{
			// Arrange
			var unitOfWork = new UnitOfWork(context);
			var expected = new Genre { Name = "Testing Genre", Id = Guid.NewGuid() };
			//act
			await unitOfWork.GenreRepository.AddAsync(expected);
			await unitOfWork.SaveAsync();
			var result = await unitOfWork.GenreRepository.GetByIDAsync(expected.Id);
			//assert
			Assert.That(result, Is.EqualTo(expected).Using(new GenreEqualityComparer()), message: "AddAsync method is inccorect");
		}

		[Test]
		public async Task GenreRepositoryDeleteRemovesSingleValue()
		{
			// Arrange
			var unitOfWork = new UnitOfWork(context);
			var expected = DBSeeder.Genres[0];
			//act
			unitOfWork.GenreRepository.Delete(expected);
			await unitOfWork.SaveAsync();
			var result = await unitOfWork.GenreRepository.GetByIDAsync(expected.Id);
			//assert
			Assert.That(result, Is.Null, message: "Delete method is inccorect");
		}

		[Test]
		public async Task GenreRepositoryDeleteByIdAsyncRemovesSingleValue()
		{
			// Arrange
			var unitOfWork = new UnitOfWork(context);
			var expected = DBSeeder.Genres[0];
			//act
			await unitOfWork.GenreRepository.DeleteByIdAsync(expected.Id);
			await unitOfWork.SaveAsync();
			var result = await unitOfWork.GenreRepository.GetByIDAsync(expected.Id);
			//assert
			Assert.That(result, Is.Null, message: "DeleteByIdAsync method is inccorect");
		}

		[Test]
		public async Task GenreRepositoryUpdateUpdatesValueInDatabase()
		{
			// Arrange
			var unitOfWork = new UnitOfWork(context);
			var genre = DBSeeder.Genres[0];
			genre.Name = "Updated Genre";
			//act
			unitOfWork.GenreRepository.Update(genre);
			await unitOfWork.SaveAsync();
			var result = await unitOfWork.GenreRepository.GetByIDAsync(genre.Id);
			//assert
			Assert.That(result, Is.EqualTo(genre).Using(new GenreEqualityComparer()), message: "Update method is inccorect");
		}

		[Test]
		public async Task AddingGenreWithDuplicateNameThrowsException()
		{
			// Arrange
			var unitOfWork = new UnitOfWork(context);
			var genre = new Genre { Name = "Strategy", Id = Guid.NewGuid() };
			//act
			await unitOfWork.GenreRepository.AddAsync(genre);
			//assert
			Assert.ThrowsAsync<DbUpdateException>(async () => await unitOfWork.SaveAsync());
		}

		[Test]
		public void UpdatingGenreWithDuplicateNameThrowsException()
		{
			// Arrange
			var unitOfWork = new UnitOfWork(context);
			var genre = DBSeeder.Genres[0];
			genre.Name = "Action";
			//act
			unitOfWork.GenreRepository.Update(genre);
			//assert
			Assert.ThrowsAsync<DbUpdateException>(async () => await unitOfWork.SaveAsync());
		}
	}
}
