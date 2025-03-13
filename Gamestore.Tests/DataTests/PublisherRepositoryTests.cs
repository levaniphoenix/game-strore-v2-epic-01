using Data.Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.Tests.DataTests;

public class PublisherRepositoryTests
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
		var expected = DBSeeder.Publishers;
		//act
		var result = await unitOfWork.PublisherRepository.GetAllAsync();
		//assert
		Assert.That(result, Is.EquivalentTo(expected).Using(new PublisherEqualityComparer()), message: "GetAllAsync method is inccorect");
	}
	[Test]
	public async Task GetAllReturnsAllValuesWithNavigationProps()
	{
		// Arrange
		var unitOfWork = new UnitOfWork(context);
		var expected = DBSeeder.Publishers;

		var expectedGames = from g in DBSeeder.Games
								 join p in DBSeeder.Publishers on g.PublisherId equals p.Id
								 orderby g.Id
								 select g;

		//act
		var result = await unitOfWork.PublisherRepository.GetAllAsync(includeProperties:"Games");
		//assert
		Assert.Multiple(() =>
		{
			Assert.That(result.SelectMany(i => i.Games).OrderBy(i => i.Id),
			Is.EquivalentTo(expectedGames).Using(new GameEqualityComparer()), message: "GetAllAsync does not return included entities");

			Assert.That(result, Is.EquivalentTo(expected).Using(new PublisherEqualityComparer()), message: "GetAllAsync method is inccorect");
		});
	}

	[Test]
	public async Task GetByIDReturnsSingleValue()
	{
		// Arrange
		var unitOfWork = new UnitOfWork(context);
		var expected = DBSeeder.Publishers[0];
		//act
		var result = await unitOfWork.PublisherRepository.GetByIDAsync(expected.Id);
		//assert
		Assert.That(result, Is.EqualTo(expected).Using(new PublisherEqualityComparer()), message: "GetByIDAsync method is inccorect");
	}

	[Test]
	public async Task GetByNameReturnsSingleValue()
	{
		// Arrange
		var unitOfWork = new UnitOfWork(context);
		var expected = DBSeeder.Publishers[0];
		//act
		var result = await unitOfWork.PublisherRepository.GetAllAsync(g => g.CompanyName == expected.CompanyName);
		//assert
		Assert.That(result.Count, Is.EqualTo(1));
		Assert.That(result.First, Is.EqualTo(expected).Using(new PublisherEqualityComparer()), message: "getting by the Name is inccorect");
	}
	[Test]
	public async Task AddAsyncAddsSingleValue()
	{
		// Arrange
		var unitOfWork = new UnitOfWork(context);
		var newPublisher = new Publisher
		{
			Id = Guid.NewGuid(),
			CompanyName = "New Publisher",
			HomePage = "http://newpublisher.com",
			Description = "A new game publisher"
		};

		// Act
		await unitOfWork.PublisherRepository.AddAsync(newPublisher);
		await unitOfWork.SaveAsync();
		var result = await unitOfWork.PublisherRepository.GetByIDAsync(newPublisher.Id);

		// Assert
		Assert.That(result, Is.EqualTo(newPublisher).Using(new PublisherEqualityComparer()), message: "AddAsync method is incorrect");
	}
	[Test]
	public async Task PublisherRepositoryDeleteAsyncRemovesSingleValue()
	{
		// Arrange
		var unitOfWork = new UnitOfWork(context);
		var expected = DBSeeder.Publishers[0];
		//act
		unitOfWork.PublisherRepository.Delete(expected);
		await unitOfWork.SaveAsync();
		var result = await unitOfWork.PublisherRepository.GetByIDAsync(expected.Id);
		//assert
		Assert.That(result, Is.Null);
	}
	[Test]
	public async Task UpdateAsyncUpdatesValueInDatabase()
	{
		// Arrange
		var unitOfWork = new UnitOfWork(context);
		var publisher = DBSeeder.Publishers[0];
		publisher.CompanyName = "Updated Publisher";
		//act
		unitOfWork.PublisherRepository.Update(publisher);
		await unitOfWork.SaveAsync();
		var result = await unitOfWork.PublisherRepository.GetByIDAsync(publisher.Id);
		//assert
		Assert.That(result, Is.EqualTo(publisher).Using(new PublisherEqualityComparer()), message: "UpdateAsync method is inccorect");
	}
	[Test]
	public async Task DeleteByIdRemovesValueFromDataBase()
	{
		// Arrange
		var unitOfWork = new UnitOfWork(context);
		var publisherToDelete = DBSeeder.Publishers[0];

		// Act
		await unitOfWork.PublisherRepository.DeleteByIdAsync(publisherToDelete.Id);
		await unitOfWork.SaveAsync();
		var result = await unitOfWork.PublisherRepository.GetByIDAsync(publisherToDelete.Id);

		// Assert
		Assert.That(result, Is.Null, message: "DeleteByIdAsync method is incorrect");
	}
	[Test]
	public async Task AddingPublisherWithDuplicateCompanyNameThrowsException()
	{
		// Arrange
		var unitOfWork = new UnitOfWork(context);
		var publisher = new Publisher
		{
			Id = Guid.NewGuid(),
			CompanyName = "Test Publisher",
			HomePage = "http://publisher1.com",
			Description = "A game publisher"
		};
		// Act
		await unitOfWork.PublisherRepository.AddAsync(publisher);
		// Assert
		Assert.ThrowsAsync<DbUpdateException>(unitOfWork.SaveAsync);
	}
	[Test]
	public void UpdatingPublisherWithDuplicateCompanyNameThrowsException()
	{
		// Arrange
		var unitOfWork = new UnitOfWork(context);
		var publisher = DBSeeder.Publishers[0];
		publisher.CompanyName = "Test Publisher 2";
		unitOfWork.PublisherRepository.Update(publisher);
		// Act
		Assert.ThrowsAsync<DbUpdateException>(unitOfWork.SaveAsync);
	}
}
