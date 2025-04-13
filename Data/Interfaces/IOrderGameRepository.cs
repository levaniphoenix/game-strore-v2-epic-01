using Data.Entities;

namespace Data.Interfaces;

public interface IOrderGameRepository
{
	Task<OrderGame?> GetByIDAsync(Guid orderId, Guid productId);

	Task DeleteByIdAsync(Guid orderId, Guid productId);
}
