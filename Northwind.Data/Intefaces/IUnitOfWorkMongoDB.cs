using Northwind.Data.Entities;
using Northwind.Data.Repositories;

namespace Northwind.Data.Intefaces;

public interface IUnitOfWorkMongoDB
{
	IRepository<Category> Categories { get; }

	IRepository<Customer> Customers { get; }

	IRepository<Employee> Employees { get; }

	IRepository<Order> Orders { get; }

	IRepository<OrderDetails> OrderDetails { get; }

	IRepository<Product> Products { get; }

	IRepository<Region> Regions { get; }

	IRepository<Shipper> Shippers { get; }

	IRepository<Supplier> Suppliers { get; }

	IRepository<Territory> Territories { get; }

	IRepository<EmployeeTerritories> EmployeeTerritories { get; }
}
