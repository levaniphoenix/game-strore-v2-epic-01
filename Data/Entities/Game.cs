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

		[Required]
		public double Price { get; set; }

		[Required]
		public int UnitInStock { get; set; }

		[Required]
		public int Discount { get; set; }

		[Required]
		public Guid PublisherId { get; set; }

		[Required]
		public DateTime PublishDate { get; set; }

		public int Views { get; set; }

		public bool IsDeleted { get; set; }

		public ICollection<Platform> Platforms { get; set; } = [];

		public ICollection<Genre> Genres { get; set; } = [];

		public ICollection<OrderGame> OrderGames { get; set; } = [];

		public virtual Publisher Publisher { get; set; } = default!;

		public ICollection<Comment> Comments { get; set; } = [];
	}
}
