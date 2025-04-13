using System.Text.Json.Serialization;

namespace Business.Models;

public class UserModel
{
	public UserDetails User { get; set; }

	[JsonPropertyName("fetchedRoles")]
	public RoleModel[] Roles { get; set; }

	[JsonPropertyName("roles")]
	public Guid[] RoleIds { get; set; }

	public class UserDetails
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PasswordHash { get; set; }
		public bool IsBanned { get; set; }
		public DateTime BannedUntil { get; set; }
	}
}
