using Data.Data;
using Data.Entities;

namespace Data.Repositories
{
	internal class PlatformRepository : GenericRepository<Platform>
	{
		public PlatformRepository(GamestoreDBContext context) : base(context)
		{
		}
	}
}
