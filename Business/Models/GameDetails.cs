using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Business.Models
{
	[DataContract]
	public class GameDetails
	{
		public Guid? Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; } = default!;

		[StringLength(100)]
		public string? Key { get; set; }

		[StringLength(5000)]
		public string? Description { get; set; }

		[Required]
		public double Price { get; set; }

		[Required]
		public int UnitInStock { get; set; }

		[Required]
		public int Discount { get; set; }

		public bool IsDeleted { get; set; }
	}
}
