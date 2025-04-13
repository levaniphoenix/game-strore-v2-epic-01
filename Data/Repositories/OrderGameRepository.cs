using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class OrderGameRepository(GamestoreDBContext context) : GenericRepository<OrderGame>(context), IOrderGameRepository
{
	public async Task<OrderGame?> GetByIDAsync(Guid orderId, Guid productId)
	{
		return await dbSet.FirstOrDefaultAsync(og => og.OrderId == orderId && og.ProductId == productId);
	}
	public async Task DeleteByIdAsync(Guid orderId, Guid productId)
	{
		var orderGame = await GetByIDAsync(orderId, productId);
		if (orderGame != null)
		{
			dbSet.Remove(orderGame);
			await Context.SaveChangesAsync();
		}
	}
}
