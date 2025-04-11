namespace Business.Models;

public class UserModel
{
	public UserDetails User { get; set; }

	public RoleModel[] Roles { get; set; }

	public class UserDetails
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PasswordHash { get; set; }
		public bool IsBanned { get; set; }
		public DateTime BannedUntil { get; set; }
	}
}
