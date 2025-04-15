using MongoDB.Driver;
using Northwind.Data.Entities;

namespace Northwind.Data.Repositories;

public class OrderDetailsRepository : GenericRepository<OrderDetails>
{
	public OrderDetailsRepository(IMongoDatabase database) : base(database, "order-details")
	{
	}
}
