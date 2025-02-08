namespace Data.Entities
{
	public class GamePlatform
	{
		public Guid GameId { get; set; }
		public Game Game { get; set; } = default!;
		public Guid PlatformId { get; set; }
		public Platform Platform { get; set; } = default!;
	}
}
