using Business.Interfaces;
using Business.Models;
using Common.Options;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.Controllers;

[Route("[controller]")]
[ApiController]
public class OrdersController(IOrderService orderService) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<IEnumerable<OrderModel>>> Get()
	{
		var orders = await orderService.GetPaidAndCancelledOrdersAsync();
		return Ok(orders);
	}

	[HttpGet("cart")]
	public async Task<ActionResult<IEnumerable<OrderDetailsModel>>> GetCartAsync()
	{
		var cart = await orderService.GetCartAsync();
		return Ok(cart);
	}

	[HttpDelete("cart/{key}")]
	public async Task<ActionResult> RemoveFromCart(string key)
	{
		await orderService.RemoveFromCartAsync(key);
		return Ok();
	}

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

	[HttpGet("payment-methods")]
	public async Task<ActionResult<IEnumerable<PaymentOptions>>> GetPaymentMethods()
	{
		var paymentMethods = await orderService.GetPaymentMethodsAsync();
		return Ok(paymentMethods);
	}

	[HttpPost("payment")]
	public async Task<IActionResult> Pay([FromBody] PaymentRequestModel request)
	{
		return await orderService.ProcessPaymentAsync(request.Method, request.Model);
	}
}
