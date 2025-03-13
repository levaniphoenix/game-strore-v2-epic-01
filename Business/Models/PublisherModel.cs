using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Business.Models;

[DataContract]
public class PublisherModel
{
	[DataMember(Name = "publisher")]
	public PublisherDetails Publisher { get; set; } = new PublisherDetails();

	public class PublisherDetails
	{
		public Guid? Id { get; set; }
		
		[Required]
		[StringLength(100 )]
		public string CompanyName { get; set; } = default!;

		public string? HomePage { get; set; }

		public string? Description { get; set; }
	}
}
