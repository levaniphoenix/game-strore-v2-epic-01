using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
	public class Platform
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		[Column(TypeName = "VARCHAR(100)")]
		//unique
		public String Type { get; set; } = default!;

		public ICollection<Game> Games { get; set; } = new List<Game>();
	}
}
