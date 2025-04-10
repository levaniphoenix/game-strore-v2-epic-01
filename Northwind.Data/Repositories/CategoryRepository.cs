using Northwind.Data.Entities;
using MongoDB.Driver;

namespace Northwind.Data.Repositories;

public class CategoryRepository : GenericRepository<Category>
{
	public CategoryRepository(IMongoDatabase database) : base(database, "categories")
	{
	}
}
