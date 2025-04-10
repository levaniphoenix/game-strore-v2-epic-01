using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Northwind.Data.Entities;

public class Territory
{
	[BsonId]
	public ObjectId Id { get; set; }
	public int TerritoryID { get; set; }
	public string TerritoryDescription { get; set; }
	public int RegionID { get; set; }
}
