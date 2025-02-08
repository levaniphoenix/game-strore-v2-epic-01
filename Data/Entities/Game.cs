using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
	public class Game
	{
		[Key]
		[Required]
		public Guid Id { get; set; }

		[Required]
		[Column(TypeName = "VARCHAR(100)")]
		public String Name { get; set; } = default!;

		[Required]
		//unique
		public String Key { get; set; } = default!;

		public String? Description { get; set; }
	}
}
