using System.Text.Json.Serialization;

namespace Business.Models
{
	public class GameModel
	{
		[JsonPropertyName("game")]
		public GameDetails Game { get; set; } = new GameDetails();

		[JsonPropertyName("genres")]
		public ICollection<Guid>? GenreIds { get; set; } = [];

		[JsonPropertyName("platforms")]
		public ICollection<Guid>? PlatformIds { get; set; } = [];

		[JsonPropertyName("publisher")]
		public Guid PublisherId { get; set; } = Guid.Empty;
	}
}
