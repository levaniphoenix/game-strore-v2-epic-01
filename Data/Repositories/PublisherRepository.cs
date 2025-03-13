using Data.Data;
using Data.Entities;

namespace Data.Repositories;

public class PublisherRepository(GamestoreDBContext context) : GenericRepository<Publisher>(context)
{
}
