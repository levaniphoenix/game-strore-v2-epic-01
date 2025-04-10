using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Northwind.Data.Entities;

public class Shipper
{
	[BsonId]
	public ObjectId Id { get; set; }

	public int ShipperID { get; set; }

	public string CompanyName { get; set; }

	public string Phone { get; set; }
}
