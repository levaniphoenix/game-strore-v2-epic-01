using MongoDB.Driver;
using Northwind.Data.Entities;

namespace Northwind.Data.Repositories;

public class ProductRepository : GenericRepository<Product>
{
	public ProductRepository(IMongoDatabase database) : base(database, "products")
	{
	}
}
