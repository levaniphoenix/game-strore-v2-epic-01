namespace Business.Models;

public class OrderDetailsModel
{
	public string Id => OrderID.ToString() + "&" + ProductId.ToString();

	public Guid ProductId { get; set; }

	public Guid OrderID { get; set; }

	public double Price { get; set; }

	public int Quantity { get; set; }

	public int Discount { get; set; }
}
