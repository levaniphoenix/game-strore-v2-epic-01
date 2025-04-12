using System.Text.Json;
using Business.Models;
using Common.Filters;
using Common.Options;
using Microsoft.AspNetCore.Mvc;

namespace Business.Interfaces;

public interface IOrderService
{
	Task<IEnumerable<OrderModel>> GetAllAsync();

	Task<OrderModel?> GetByIdAsync(object id);

	Task DeleteAsync(object modelId);

	Task AddToCartAsync(string key, Guid Id);

	Task RemoveFromCartAsync(string key, Guid Id);

	Task<IEnumerable<OrderModel>> GetPaidAndCancelledOrdersAsync(OrderHistoryFilter historyFilter);

	Task<IEnumerable<OrderDetailsModel>> GetCartAsync(Guid Id);

	Task<IEnumerable<OrderDetailsModel>>? GetOrderDetailsByIdAsync(object id);

	Task<PaymentOptions> GetPaymentMethodsAsync();

	Task<IActionResult> ProcessPaymentAsync(string method, JsonElement model);

	Task<IEnumerable<OrderModel>> GetOpenOrdersAsync();
}
