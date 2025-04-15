using MongoDB.Driver;
using Northwind.Data.Entities;

namespace Northwind.Data.Repositories;

public class CustomerRepository : GenericRepository<Customer>
{
	public CustomerRepository(IMongoDatabase database) : base(database, "customers")
	{
	}
}
