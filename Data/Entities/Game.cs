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
		public string Name { get; set; } = default!;

		[Required]
		//unique
		public string Key { get; set; } = default!;

		[Column(TypeName = "VARCHAR(5000)")]
		public string? Description { get; set; }

		public ICollection<Platform> Platforms { get; set; } = [];

		public ICollection<Genre> Genres { get; set; } = [];
	}
}
