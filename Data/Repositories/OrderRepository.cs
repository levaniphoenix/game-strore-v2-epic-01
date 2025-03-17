using Data.Data;
using Data.Entities;

namespace Data.Repositories;

public class OrderRepository(GamestoreDBContext context) : GenericRepository<Order>(context)
{
}
