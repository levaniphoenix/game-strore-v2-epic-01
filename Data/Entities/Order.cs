using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class Order
{
	[Key]
	[Required]
	public Guid Id { get; set; }

	public DateTime? Date { get; set; }

	[Required]
	public Guid CustomerId { get; set; }

	[Required]
	public OrderStatus Status { get; set; }

	public ICollection<OrderGame> OrderDetails { get; set; } = [];
}
