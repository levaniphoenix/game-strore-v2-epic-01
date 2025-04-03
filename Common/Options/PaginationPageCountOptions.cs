namespace Common.Options;

public static class PaginationPageCountOptions
{
	public const int Ten = 10;
	public const int Twenty = 20;
	public const int Fifty = 50;
	public const int Hundred = 100;

	public const string All = "all";

	public static readonly string[] Values = { 
		Ten.ToString(), 
		Twenty.ToString(), 
		Fifty.ToString(), 
		Hundred.ToString(), 
		All };
}
