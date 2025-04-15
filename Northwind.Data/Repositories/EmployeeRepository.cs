using MongoDB.Driver;
using Northwind.Data.Entities;

namespace Northwind.Data.Repositories;

public class EmployeeRepository : GenericRepository<Employee>
{
	public EmployeeRepository(IMongoDatabase database) : base(database, "employees")
	{
	}
}
