using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
	public class Genre
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		//unique
		public String Name { get; set; } = default!;

		public Guid? ParentGenreId { get; set; }
	}
}
