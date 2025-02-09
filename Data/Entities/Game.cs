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

		[Column(TypeName = "VARCHAR(5000)")]
		public String? Description { get; set; }

		public ICollection<Platform> Platforms { get; set; } = new List<Platform>();

		public ICollection<Genre> Genres { get; set; } = new List<Genre>();
	}
}
