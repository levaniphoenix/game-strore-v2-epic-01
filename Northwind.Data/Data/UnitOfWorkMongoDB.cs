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

	public UnitOfWorkMongoDB(string connectionString, string databaseName)
	{
		var mongoClient = new MongoClient(connectionString);
		var mongoDatabase = mongoClient.GetDatabase(databaseName);
		Categories = new CategoryRepository(mongoDatabase);
		Customers = new GenericRepository<Customer>(mongoDatabase,"customers");
		Employees = new GenericRepository<Employee>(mongoDatabase, "employees");
		Orders = new GenericRepository<Order>(mongoDatabase, "orders");
		OrderDetails = new GenericRepository<OrderDetails>(mongoDatabase, "order-details");
		Products = new GenericRepository<Product>(mongoDatabase, "products");
		Regions = new GenericRepository<Region>(mongoDatabase, "regions");
		Shippers = new GenericRepository<Shipper>(mongoDatabase, "shippers");
		Suppliers = new GenericRepository<Supplier>(mongoDatabase, "suppliers");
		Territories = new GenericRepository<Territory>(mongoDatabase, "territories");
	}
}
