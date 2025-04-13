using System.Text.Json;
using Business.Interfaces;
using Business.Models;
using Common.Filters;
using Common.Options;
using Gamestore.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.Controllers;

[Route("[controller]")]
[ApiController]
public class OrdersController(IOrderService orderService) : ControllerBase
{
	[Authorize(Policy = "ManagerPolicy")]
	[HttpGet("history")]
	public async Task<ActionResult<IEnumerable<OrderModel>>> Get([FromQuery]OrderHistoryFilter historyFilter)
	{
		var orders = await orderService.GetPaidAndCancelledOrdersAsync(historyFilter);
		return Ok(orders);
	}

	[Authorize(Policy = "ManagerPolicy")]
	[HttpGet]
	public async Task<ActionResult<IEnumerable<OrderModel>>> Get()
	{
		IEnumerable<OrderModel> orders = await orderService.GetOpenOrdersAsync();
		return Ok(orders);
	}

	[Authorize(Policy = "UserPolicy")]
	[HttpGet("cart")]
	public async Task<ActionResult<IEnumerable<OrderDetailsModel>>> GetCartAsync()
	{
		var userId = JwtHelper.GetUserId(HttpContext);
		var cart = await orderService.GetCartAsync(userId);
		return Ok(cart);
	}

	[Authorize(Policy = "UserPolicy")]
	[HttpDelete("cart/{key}")]
	public async Task<ActionResult> RemoveFromCart(string key)
	{
		var userId = JwtHelper.GetUserId(HttpContext);
		await orderService.RemoveFromCartAsync(key, userId);
		return Ok();
	}

	[Authorize(Policy = "UserPolicy")]
	[HttpDelete("details/{compositeKey}")]
	public async Task<ActionResult> RemoveOrderDetail(string compositeKey)
	{
		var keys = compositeKey.Split('&');
		if (keys.Length != 2 || !Guid.TryParse(keys[0], out var orderId) || !Guid.TryParse(keys[1], out var productId))
		{
			return BadRequest("Invalid composite key format.");
		}

		await orderService.RemoveOrderDetailAsync(orderId, productId);
		return Ok();
	}

	[Authorize(Policy = "ManagerPolicy")]
	[HttpGet("{id}/details")]
	public async Task<ActionResult<IEnumerable<OrderDetailsModel>>> GetOrderDetailsByIdAsync(Guid id)
	{
		var orderDetails = await orderService.GetOrderDetailsByIdAsync(id);
		if (orderDetails is null)
		{
			return NotFound();
		}

		return Ok(orderDetails);
	}

	[Authorize(Policy = "ManagerPolicy")]
	[HttpGet("{id}")]
	public async Task<ActionResult<OrderModel>> GetById(Guid id)
	{
		var order = await orderService.GetByIdAsync(id);
		if (order is null)
		{
			return NotFound();
		}

		return Ok(order);
	}

	[AllowAnonymous]
	[HttpGet("payment-methods")]
	public async Task<ActionResult<IEnumerable<PaymentOptions>>> GetPaymentMethods()
	{
		var paymentMethods = await orderService.GetPaymentMethodsAsync();
		return Ok(paymentMethods);
	}

	[Authorize(Policy = "UserPolicy")]
	[HttpPost("payment")]
	public async Task<IActionResult> Pay([FromBody] PaymentRequestModel request)
	{
		return await orderService.ProcessPaymentAsync(request.Method, request.Model);
	}

	[Authorize(Policy = "ManagerPolicy")]
	[HttpPost("{OrderId}/details/{key}")]
	public async Task<IActionResult> AddGameToCart(string key, Guid orderId)
	{
		await orderService.AddGameToAnyCartAsync(orderId, key);
		return Ok();
	}

	[Authorize(Policy = "ManagerPolicy")]
	[HttpPatch("details/{compositeKey}/quantity")]
	public async Task<IActionResult> UpdateOrderDetailQuantity(string compositeKey, [FromBody] Dictionary<string, object> data)
	{
		var keys = compositeKey.Split('&');
		if (keys.Length != 2 || !Guid.TryParse(keys[0], out var orderId) || !Guid.TryParse(keys[1], out var productId))
		{
			return BadRequest("Invalid composite key format.");
		}

		if (!data.TryGetValue("count", out var quantityObj) || quantityObj is not JsonElement quantityElement || !quantityElement.TryGetInt32(out var count))
		{
			return BadRequest("Invalid count format.");
		}

		await orderService.UpdateOrderDetailQuantityAsync(orderId, productId, count);
		return Ok();
	}
}
