using Data.Data;
using Data.Entities;

namespace Data.Repositories
{
	public class GameRepository(GamestoreDBContext context) : GenericRepository<Game>(context)
	{
	}
}
