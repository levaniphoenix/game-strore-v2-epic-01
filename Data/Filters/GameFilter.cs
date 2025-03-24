namespace Data.Filters;

public class GameFilter
{
	public List<Guid>? Genres { get; set; }
	public List<Guid>? Platforms { get; set; }
	public List<Guid>? Publishers { get; set; }
	public double? MaxPrice { get; set; }
	public double? MinPrice { get; set; }
	public string? DatePublishing { get; set; }
	public string? Sort { get; set; }
	public int Page { get; set; } = 1; // Default to page 1
	public string? PageCount { get; set; }
	public string? Name { get; set; }
}
