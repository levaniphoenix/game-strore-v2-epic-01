using Data.Data;
using Data.Entities;

namespace Data.Repositories;
public class UserRepository : GenericRepository<User>
{
	public UserRepository(GamestoreDBContext context) : base(context)
	{
	}
}
