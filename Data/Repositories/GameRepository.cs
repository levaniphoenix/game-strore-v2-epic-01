using Data.Data;
using Data.Entities;

namespace Data.Repositories
{
	internal class GameRepository : GenericRepository<Game>
	{
		public GameRepository(GamestoreDBContext context) : base(context)
		{
		}
	}
}
