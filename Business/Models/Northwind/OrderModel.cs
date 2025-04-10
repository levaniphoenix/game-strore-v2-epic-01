namespace Business.Models.Northwind;

public class OrderModel
{
	public string Id { get; set; }

	public int OrderId { get; set; }

	public string CustomerID { get; set; }

	public int EmployeeID { get; set; }

	public string OrderDate { get; set; }

	public string RequiredDate { get; set; }

	public string ShippedDate { get; set; }

	public int ShipVia { get; set; }

	public decimal Freight { get; set; }

	public string ShipName { get; set; }

	public string ShipAddress { get; set; }

	public string ShipCity { get; set; }

	public string ShipRegion { get; set; }

	public int ShipPostalCode { get; set; }

	public string ShipCountry { get; set; }
}
