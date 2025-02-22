using Data.Data;
using Data.Entities;

namespace Data.Repositories
{
	public class PlatformRepository(GamestoreDBContext context) : GenericRepository<Platform>(context)
	{
	}
}
