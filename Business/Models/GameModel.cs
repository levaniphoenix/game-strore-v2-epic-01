using Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
	public class GameModel
	{
		public Guid Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; } = default!;

		[StringLength(100)]
		public string? Key { get; set; }

		[StringLength(5000)]
		public string? Description { get; set; }

		public ICollection<Guid> PlatformIds { get; set; } = new List<Guid>();

		public ICollection<Guid> GenreIds { get; set; } = new List<Guid>();
	}
}
