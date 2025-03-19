using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace Business.Interfaces;

public interface IOrderService : ICrud<OrderModel>
{
	Task AddToCartAsync(string key);

	Task RemoveFromCartAsync(string key);

	Task<IEnumerable<OrderModel>> GetPaidAndCancelledOrdersAsync();

	Task<IEnumerable<OrderDetailsModel>> GetCartAsync();

	Task<IEnumerable<OrderDetailsModel>>? GetOrderDetailsByIdAsync(object id);

	Task<PaymentMethodsModel> GetPaymentMethodsAsync();

	Task<IActionResult> ProcessPaymentAsync(string method, dynamic model = null);
}
