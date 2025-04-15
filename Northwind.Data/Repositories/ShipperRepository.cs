using MongoDB.Driver;
using Northwind.Data.Entities;

namespace Northwind.Data.Repositories;

public class ShipperRepository : GenericRepository<Shipper>
{
	public ShipperRepository(IMongoDatabase database) : base(database, "shippers")
	{
	}
}
