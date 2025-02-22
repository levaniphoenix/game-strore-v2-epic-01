using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
	public class Genre
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		[Column(TypeName = "VARCHAR(50)")]
		//unique
		public string Name { get; set; } = default!;

		public Guid? ParentGenreId { get; set; }

		public virtual Genre Parent { get; set; } = default!;

		public virtual ICollection<Genre> Children { get; set; } = [];

		public ICollection<Game> Games { get; set; } = [];
	}
}
