using Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
	public class PlatformModel
	{
		public Guid Id { get; set; }

		[Required]
		public String Type { get; set; } = default!;

		public ICollection<Game> Games { get; set; } = new List<Game>();
	}
}
