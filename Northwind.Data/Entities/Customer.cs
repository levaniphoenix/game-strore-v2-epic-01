﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Northwind.Data.Entities;

public class Customer
{
	[BsonId]
	public ObjectId Id { get; set; }

	public string CustomerID { get; set; }

	public string CompanyName { get; set; }

	public string ContactName { get; set; }

	public string ContactTitle { get; set; }

	public string Address { get; set; }

	public string City { get; set; }

	public string Region { get; set; }

	public int PostalCode { get; set; }

	public string Country { get; set; }

	public string Phone { get; set; }

	public string Fax { get; set; }
}
