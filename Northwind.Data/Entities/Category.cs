using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Northwind.Data.Entities;

[BsonIgnoreExtraElements]
public class Category
{
	[BsonId]
	public ObjectId Id { get; set; }

	public int CategoryID { get; set; }

	public string CategoryName { get; set; }

	public string Description { get; set; }

	public string Picture { get; set; }
}

