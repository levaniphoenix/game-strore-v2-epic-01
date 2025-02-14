using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
	public class GameGenre
	{
		[Required]
		public Guid GameId { get; set; }
		public Game Game { get; set; } = default!;
		[Required]
		public Guid GenreId { get; set; }
		public Genre Genre { get; set; } = default!;
	}
}
