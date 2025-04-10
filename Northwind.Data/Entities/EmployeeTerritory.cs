using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Northwind.Data.Entities;

public class EmployeeTerritory
{
	[BsonId]
	public ObjectId Id { get; set; }
	public int EmployeeID { get; set; }
	public string TerritoryID { get; set; }
}
