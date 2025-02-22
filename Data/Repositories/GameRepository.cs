using Data.Data;
using Data.Entities;

namespace Data.Repositories
{
	internal sealed class GameRepository(GamestoreDBContext context) : GenericRepository<Game>(context)
	{
	}
}
