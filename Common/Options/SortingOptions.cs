namespace Common.Options;

public static class SortingOptions
{
	public const string PriceAscending = "Price ASC";
	public const string PriceDescending = "Price DESC";
	public const string MostPopular = "Most popular";
	public const string MostCommented = "Most commented";
	public const string New = "New";
	
	public static readonly string[] Values = { 
		PriceAscending, 
		PriceDescending,
		MostPopular,
		MostCommented,
		New
	};
}
