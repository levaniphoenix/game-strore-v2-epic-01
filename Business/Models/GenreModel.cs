using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Business.Models
{
	[DataContract]
	public class GenreModel
	{
		[DataMember(Name = "genre")]
		public GenreDetails Genre { get; set; } = new GenreDetails();

		[DataContract]
		public class GenreDetails
		{
			public Guid? Id { get; set; }

			[Required]
			[StringLength(50)]
			public string Name { get; set; } = default!;

			public Guid? ParentGenreId { get; set; }
		}
	}
}
