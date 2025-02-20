using Business.Models;

namespace Business.Interfaces
{
	public interface IGenreService : ICrud<GenreModel>
	{
		Task<IEnumerable<GameModel?>> GetGamesByGenreIdAsync(Guid id);

		Task<IEnumerable<GenreModel?>> GetGenresByParentId(Guid id);
	}
}
