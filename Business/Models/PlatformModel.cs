using Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Business.Models
{
	[DataContract]
	public class PlatformModel
	{
		[DataMember(Name = "platform")]
		public PlatformDetails Platform { get; set; } = new PlatformDetails();

		[DataContract]
		public class PlatformDetails
		{
			public Guid? Id { get; set; }
			
			[Required]
			[StringLength(50)]
			public string Type { get; set; } = default!;
		}
	}
}
