namespace Common.Options;

public static class RolePermissions
{
	public const string AddGame = "AddGame";
	public const string UpdateGame = "UpdateGame";
	public const string DeleteGame = "DeleteGame";
	public const string ViewGame = "ViewGame";

	public static readonly string[] Values = {
		AddGame,
		UpdateGame,
		DeleteGame,
		ViewGame
	};
}
