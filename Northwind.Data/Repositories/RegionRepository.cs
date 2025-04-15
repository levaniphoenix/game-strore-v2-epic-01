using MongoDB.Driver;
using Northwind.Data.Entities;

namespace Northwind.Data.Repositories;

public class RegionRepository : GenericRepository<Region>
{
	public RegionRepository(IMongoDatabase database) : base(database, "regions")
	{
	}
}
