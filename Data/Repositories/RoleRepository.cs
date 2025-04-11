using Data.Data;
using Data.Entities;

namespace Data.Repositories;

public class RoleRepository : GenericRepository<Role>
{
	public RoleRepository(GamestoreDBContext context) : base(context)
	{
	}
}
