using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Northwind.Data.Entities;

public class OrderDetails
{
	[BsonId]
	public ObjectId Id { get; set; }

	public int OrderID { get; set; }

	public int ProductID { get; set; }

	public int UnitPrice { get; set; }

	public int Quantity { get; set; }

	public float Discount { get; set; }

}
