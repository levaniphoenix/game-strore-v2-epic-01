using Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
	public class GameModel
	{
		public Guid Id { get; set; }

		[Required]
		[StringLength(100)]
		public String Name { get; set; } = default!;

		[StringLength(5000)]
		public String? Description { get; set; }

		public ICollection<Platform> Platforms { get; set; } = new List<Platform>();

		public ICollection<Genre> Genres { get; set; } = new List<Genre>();
	}
}
