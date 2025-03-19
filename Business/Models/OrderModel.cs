namespace Business.Models;

public class OrderModel
{
	public Guid Id { get; set; }

	public DateTime? Date { get; set; }

	public Guid CustomerId { get; set; }
}
