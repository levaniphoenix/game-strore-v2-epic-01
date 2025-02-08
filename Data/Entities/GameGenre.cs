namespace Data.Entities
{
	public class GameGenre
	{
		public Guid GameId { get; set; }
		public Game Game { get; set; } = default!;
		public Guid GenreId { get; set; }
		public Genre Genre { get; set; } = default!;
	}
}
