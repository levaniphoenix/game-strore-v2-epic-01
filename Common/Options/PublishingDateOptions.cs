namespace Common.Options;

/// This class defines the options for filtering games by their publishing date.
public static class PublishingDateOptions
{
	public const string LastWeek = "last week";
	public const string LastMonth = "last month";
	public const string LastYear = "last year";
	public const string LastTwoYears = "2 years";
	public const string LastThreeYears = "3 years";

	public static string[] Values => new[]
	{
		LastWeek,
		LastMonth,
		LastYear,
		LastTwoYears,
		LastThreeYears
	};
}
