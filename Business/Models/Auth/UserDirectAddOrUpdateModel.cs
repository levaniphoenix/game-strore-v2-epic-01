using System.Text.Json.Serialization;

namespace Business.Models.Auth;

public class UserDirectAddOrUpdateModel
{
	public UserParams User { get; set; }

	public string Password { get; set; }

	[JsonPropertyName("roles")]
	public Guid[] RoleIds { get; set; }

	public class UserParams
	{
		public Guid? Id { get; set; }
		public string Name { get; set; }
	}
}
