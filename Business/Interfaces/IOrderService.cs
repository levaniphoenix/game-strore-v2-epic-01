using System.Text.Json;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace Business.Interfaces;

public interface IOrderService
{
	Task<IEnumerable<OrderModel>> GetAllAsync();

	Task<OrderModel?> GetByIdAsync(object id);

	Task DeleteAsync(object modelId);

	Task AddToCartAsync(string key);

	Task RemoveFromCartAsync(string key);

	Task<IEnumerable<OrderModel>> GetPaidAndCancelledOrdersAsync();

	Task<IEnumerable<OrderDetailsModel>> GetCartAsync();

	Task<IEnumerable<OrderDetailsModel>>? GetOrderDetailsByIdAsync(object id);

	Task<PaymentMethodsModel> GetPaymentMethodsAsync();

	Task<IActionResult> ProcessPaymentAsync(string method, JsonElement model);
}
