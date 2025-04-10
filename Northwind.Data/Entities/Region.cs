using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Northwind.Data.Entities;

public class Region
{
	[BsonId]
	public ObjectId Id { get; set; }
	
	public int RegionID { get; set; }

	public string RegionDescription { get; set; }
}
