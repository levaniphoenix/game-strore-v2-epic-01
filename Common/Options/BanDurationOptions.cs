namespace Common.Options;

public static class BanDurationOptions
{
	public const string OneHour = "1 hour";
	public const string OneDay = "1 day";
	public const string OneWeek = "1 week";
	public const string OneMonth = "1 month";
	public const string Permanent = "permanent";

	public static string[] Values => new[]
	{
		OneHour,
		OneDay,
		OneWeek,
		OneMonth,
		Permanent
	};
}
