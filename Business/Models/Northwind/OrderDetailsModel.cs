namespace Business.Models.Northwind;

public class OrderDetailsModel
{
	public string Id { get; set; }

	public int OrderID { get; set; }

	public int ProductID { get; set; }

	public int UnitPrice { get; set; }

	public int Quantity { get; set; }

	public float Discount { get; set; }

}
