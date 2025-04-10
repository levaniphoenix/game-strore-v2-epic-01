using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Northwind.Data.Entities;
public class Product
{
	[BsonId]
	public ObjectId Id { get; set; }

	public int ProductID { get; set; }

	public string ProductName { get; set; }

	public int SupplierID { get; set; }

	public int CategoryID { get; set; }

	public string QuantityPerUnit { get; set; }

	public double UnitPrice { get; set; }

	public int UnitsInStock { get; set; }

	public int UnitsOnOrder { get; set; }

	public int ReorderLevel { get; set; }

	public bool Discontinued { get; set; }
}
