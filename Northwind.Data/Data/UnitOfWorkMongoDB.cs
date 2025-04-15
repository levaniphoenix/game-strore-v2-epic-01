using MongoDB.Driver;
using Northwind.Data.Repositories;
using Northwind.Data.Intefaces;
using Northwind.Data.Entities;

namespace Northwind.Data.Data;

public class UnitOfWorkMongoDB : IUnitOfWorkMongoDB
{
	public IRepository<Category> Categories { get; }

	public IRepository<Customer> Customers { get; }

	public IRepository<Employee> Employees { get; }

	public IRepository<Order> Orders { get; }

	public IRepository<OrderDetails> OrderDetails { get; }

	public IRepository<Product> Products { get; }

	public IRepository<Region> Regions { get; }

	public IRepository<Shipper> Shippers { get; }

	public IRepository<Supplier> Suppliers { get; }

	public IRepository<Territory> Territories { get; }

	public IRepository<EmployeeTerritories> EmployeeTerritories { get; }

	public UnitOfWorkMongoDB(string connectionString, string databaseName)
	{
		var mongoClient = new MongoClient(connectionString);
		var mongoDatabase = mongoClient.GetDatabase(databaseName);
		Categories = new CategoryRepository(mongoDatabase);
		Customers = new CustomerRepository(mongoDatabase);
		Employees = new EmployeeRepository(mongoDatabase);
		Orders = new OrderRepository(mongoDatabase);
		OrderDetails = new OrderDetailsRepository(mongoDatabase);
		Products = new ProductRepository(mongoDatabase);
		Regions = new RegionRepository(mongoDatabase);
		Shippers = new ShipperRepository(mongoDatabase);
		Suppliers = new SupplierRepository(mongoDatabase);
		Territories = new TerritoryRepository(mongoDatabase);
		EmployeeTerritories = new EmployeeTerritoriesRepository(mongoDatabase);
	}
}
