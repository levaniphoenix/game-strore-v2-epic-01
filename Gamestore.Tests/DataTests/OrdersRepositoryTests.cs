using Data.Data;
using Data.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.Tests.DataTests;

[TestFixture]
public class OrdersRepositoryTests
{
	private GamestoreDBContext context;

	private readonly Guid CustomerId = Guid.Parse("00000000-0000-0000-0000-000000000000");

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
	public async Task GetByIDReturnsSingleValue()
	{
		// Arrange
		var unitOfWork = new UnitOfWork(context);
		var expected = DBSeeder.Orders[0];
		//act
		var result = await unitOfWork.OrderRepository.GetByIDAsync(expected.Id);
		//assert
		result.Should().BeEquivalentTo(expected);
	}

	[Test]
	public async Task GetByCustomerIdReturnsSingleValue()
	{
		var unitOfWork = new UnitOfWork(context);

		var expected = DBSeeder.Orders[0];

		var result = (await unitOfWork.OrderRepository.GetAllAsync(o => o.CustomerId == expected.CustomerId)).SingleOrDefault();

		result.Should().BeEquivalentTo(expected);
	}

	[Test]
	public async Task GetAllReturnsALlValues()
	{
		var unitOfWork = new UnitOfWork(context);
		var expected = DBSeeder.Orders;
		var result = await unitOfWork.OrderRepository.GetAllAsync();
		result.Should().BeEquivalentTo(expected);
	}

	[Test]
	public async Task AddAsyncAddsValueToDatabase()
	{
		var unitOfWork = new UnitOfWork(context);
		var order = new Order
		{
			CustomerId = CustomerId,
			Date = DateTime.Now,
			OrderDetails =
			[
				new OrderGame
				{
					ProductId = DBSeeder.Games[1].Id,
					Quantity = 1,
					Price = DBSeeder.Games[0].Price,
					Discount = 0
				}
			]
		};
		await unitOfWork.OrderRepository.AddAsync(order);
		await unitOfWork.SaveAsync();
		var result = await unitOfWork.OrderRepository.GetByIDAsync(order.Id);
		result.Should().BeEquivalentTo(order);
	}

	[Test]
	public async Task UpdateAsyncUpdatesValueInDatabse()
	{
		// Arrange
		var unitOfWork = new UnitOfWork(context);
		var order = DBSeeder.Orders[0];
		order.Status = OrderStatus.Paid;

		// Act
		unitOfWork.OrderRepository.Update(order);
		await unitOfWork.SaveAsync();
		var result = await unitOfWork.OrderRepository.GetByIDAsync(order.Id);

		// Assert
		result.Should().BeEquivalentTo(order);
	}

	[Test]
	public async Task DeleteAsyncRemovesValueFromDataBase()
	{
		var unitOfWork = new UnitOfWork(context);
		var order = DBSeeder.Orders[0];

		unitOfWork.OrderRepository.Delete(order);
		await unitOfWork.SaveAsync();

		var result = await unitOfWork.OrderRepository.GetByIDAsync(order.Id);
		result.Should().BeNull();
	}

	[Test]
	public async Task DeleteByIdAsyncRemovesValueFromDatabse()
	{
		var unitOfWork = new UnitOfWork(context);
		var order = DBSeeder.Orders[0];

		await unitOfWork.OrderRepository.DeleteByIdAsync(order.Id);
		await unitOfWork.SaveAsync();

		var result = await unitOfWork.OrderRepository.GetByIDAsync(order.Id);
		result.Should().BeNull();
	}
}
