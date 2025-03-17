using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class OrderGame
{
	[Required]
	public Guid OrderId { get; set; }

	public Order Order { get; set; } = default!;

	[Required]
	public Guid ProductId { get; set; }

	public Game Product { get; set; } = default!;

	[Required]
	public double Price { get; set; }

	[Required]
	public int Quantity { get; set; }

	[Required]
	public int Discount { get; set; }
}
