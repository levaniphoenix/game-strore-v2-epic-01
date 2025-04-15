using MongoDB.Driver;
using Northwind.Data.Entities;

namespace Northwind.Data.Repositories;

public class TerritoryRepository : GenericRepository<Territory>
{
	public TerritoryRepository(IMongoDatabase database) : base(database, "territories")
	{
	}
}
