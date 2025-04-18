﻿namespace Business.Models.Northwind;

public class ProductModel
{
	public ProductDetails Product { get; set; }

	public SupplierModel Supplier { get; set; }

	public CategoryModel Category { get; set; }

	public class ProductDetails
	{
		public string Id { get; set; }

		public int ProductID { get; set; }

		public string ProductName { get; set; }

		public string QuantityPerUnit { get; set; }

		public double UnitPrice { get; set; }

		public int UnitsInStock { get; set; }

		public int UnitsOnOrder { get; set; }

		public int ReorderLevel { get; set; }

		public bool Discontinued { get; set; }
	}
}
