namespace Business.Models.Auth;
public class LoginRequestModel
{
	public LoginRequest Model { get; set; }

	public class LoginRequest
	{
		public string Login { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public bool InternalAuth { get; set; }
	}
}
