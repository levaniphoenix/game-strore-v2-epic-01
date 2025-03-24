namespace Business.Models;

public class PaginatedGamesModel
{
	public IEnumerable<GameDetails> Games { get; set; }

	public int TotalPages { get; set; }

	public int CurrentPage { get; set; }
}
