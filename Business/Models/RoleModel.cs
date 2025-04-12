using System.Text.Json.Serialization;

namespace Business.Models;

public class RoleModel
{
	[JsonPropertyName("role")]
	public RoleDetails Role { get; set; }

	[JsonPropertyName("permissions")]
	public string[] Permissions { get; set; }
	
	public class RoleDetails
	{
		public Guid? Id { get; set; }
		public string Name { get; set; }
	}
}
