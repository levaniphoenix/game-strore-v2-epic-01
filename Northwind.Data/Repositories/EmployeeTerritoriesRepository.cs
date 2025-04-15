using MongoDB.Driver;

namespace Northwind.Data.Repositories;

public class EmployeeTerritoriesRepository : GenericRepository<EmployeeTerritories>
{
	public EmployeeTerritoriesRepository(IMongoDatabase database) : base(database, "employee-territories")
	{
	}
}
