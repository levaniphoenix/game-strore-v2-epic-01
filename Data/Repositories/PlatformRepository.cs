using Data.Data;
using Data.Entities;

namespace Data.Repositories
{
	internal sealed class PlatformRepository(GamestoreDBContext context) : GenericRepository<Platform>(context)
	{
	}
}
