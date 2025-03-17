using Data.Data;
using Data.Entities;

namespace Data.Repositories;

public class OrderGameRepository(GamestoreDBContext context) : GenericRepository<OrderGame>(context)
{
}
