using System.Runtime.Serialization;

namespace Business.Models
{
	[DataContract]
	public class GameModel
	{
		[DataMember(Name = "game")]
		public GameDetails Game { get; set; } = new GameDetails();

		[DataMember(Name = "genres")]
		public ICollection<Guid>? GenreIds { get; set; } = [];

		[DataMember(Name = "platforms")]
		public ICollection<Guid>? PlatformIds { get; set; } = [];

		[DataMember(Name = "publisher")]
		public Guid PublisherId { get; set; }
}
}
