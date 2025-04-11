namespace Business.Models.Auth;

public class LoginSuccessResponseModel
{
	public Guid Id { get; set; }
	public string Email { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public IEnumerable<RoleModel> Roles { get; set; }
}
