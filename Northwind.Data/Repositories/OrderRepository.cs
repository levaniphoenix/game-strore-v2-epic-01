using MongoDB.Driver;
using Northwind.Data.Entities;

namespace Northwind.Data.Repositories;

public class OrderRepository : GenericRepository<Order>
{
	public OrderRepository(IMongoDatabase database) : base(database, "orders")
	{
	}
}
