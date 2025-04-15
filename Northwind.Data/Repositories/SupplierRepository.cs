using MongoDB.Driver;
using Northwind.Data.Entities;

namespace Northwind.Data.Repositories;

public class SupplierRepository : GenericRepository<Supplier>
{
	public SupplierRepository(IMongoDatabase database) : base(database, "suppliers")
	{
	}
}
