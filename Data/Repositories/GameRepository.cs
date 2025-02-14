using Data.Data;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
	internal class GameRepository : GenericRepository<Game>
	{
		public GameRepository(GamestoreDBContext context) : base(context)
		{
		}
	}
}
