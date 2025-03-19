using AutoMapper;
using Business.Exceptions;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Business.Services;

public class OrderService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<OrderService> logger) : IOrderService
{
	public Task AddAsync(OrderModel model)
	{
		throw new NotImplementedException();
	}

	public async Task AddToCartAsync(string key)
	{
		logger.LogInformation("Adding to cart with key {Key}", key);
		
		var game = (await unitOfWork.GameRepository.GetAllAsync(g => g.Key == key)).SingleOrDefault();

		if (game == null)
		{
			logger.LogWarning("Game with key {Key} not found", key);
			return;
		}

		var cartOrder = (await unitOfWork.OrderRepository.GetAllAsync(o => o.Status == OrderStatus.Open, includeProperties: "OrderDetails")).SingleOrDefault();

		if (cartOrder == null)
		{
			logger.LogInformation("Creating new order");
			cartOrder = new Order
			{
				CustomerId = Guid.Parse("00000000-0000-0000-0000-000000000000"),
				Date = DateTime.Now,
				Status = OrderStatus.Open,
				OrderDetails = [new OrderGame { ProductId = game.Id, Quantity = 1, Price = game.Price, Discount = game.Discount }]

			};
			await unitOfWork.OrderRepository.AddAsync(cartOrder);
			await unitOfWork.SaveAsync();
			return;
		}

		// Check if product is already in cart if so increment quantity
		foreach (var item in cartOrder.OrderDetails)
		{
			if (item.ProductId == game.Id)
			{
				logger.LogInformation("Incrementing quantity of product with id {ProductId}", game.Id);
				item.Quantity++;
				unitOfWork.OrderRepository.Update(cartOrder);
				await unitOfWork.SaveAsync();
				return;
			}
		}

		// Add new product to cart
		logger.LogInformation("Adding new product to cart with id {ProductId}", game.Id);
		var orderDetail = new OrderGame {OrderId = cartOrder.Id ,ProductId = game.Id, Quantity = 1, Price = game.Price, Discount = game.Discount };
		await unitOfWork.OrderDetailsRepository.AddAsync(orderDetail);
		await unitOfWork.SaveAsync();
		logger.LogInformation("Product added to cart with id {ProductId}", game.Id);

	}

	public async Task DeleteAsync(object modelId)
	{
		await unitOfWork.OrderRepository.DeleteByIdAsync(modelId);
	}

	public async Task<IEnumerable<OrderModel>> GetAllAsync()
	{
		logger.LogInformation("Getting all orders");
		var orders = await unitOfWork.OrderRepository.GetAllAsync();
		return mapper.Map<IEnumerable<OrderModel>>(orders);
	}

	public async Task<OrderModel?> GetByIdAsync(object id)
	{
		logger.LogInformation("Getting order by id {Id}", id);
		var order = await unitOfWork.OrderRepository.GetByIDAsync(id);
		return mapper.Map<OrderModel?>(order);
	}

	public async Task<IEnumerable<OrderDetailsModel>>? GetOrderDetailsByIdAsync(object id)
	{
		logger.LogInformation("Getting order details by id {Id}", id);
		var orderDetails = await unitOfWork.OrderDetailsRepository.GetAllAsync(od => od.OrderId == (Guid)id);
		return mapper.Map<IEnumerable<OrderDetailsModel>>(orderDetails);
	}

	public async Task<IEnumerable<OrderModel>> GetPaidAndCancelledOrdersAsync()
	{
		logger.LogInformation("Getting paid and cancelled orders");
		var orders = await unitOfWork.OrderRepository.GetAllAsync(o => o.Status == OrderStatus.Paid || o.Status == OrderStatus.Cancelled);
		return mapper.Map<IEnumerable<OrderModel>>(orders);
	}

	public async Task<IEnumerable<OrderDetailsModel>> GetCartAsync()
	{
		logger.LogInformation("Getting open orders");
		var order = (await unitOfWork.OrderRepository.GetAllAsync(o => o.Status == OrderStatus.Open,includeProperties: "OrderDetails")).SingleOrDefault();
		if(order is null)
		{
			return [];
		}
		
		return mapper.Map<IEnumerable<OrderDetailsModel>>(order.OrderDetails);
	}

	public async Task RemoveFromCartAsync(string key)
	{
		logger.LogInformation("Removing from cart with key {Key}", key);

		var game = (await unitOfWork.GameRepository.GetAllAsync(g => g.Key == key)).SingleOrDefault();

		if (game == null)
		{
			logger.LogWarning("Game with key {Key} not found", key);
			return;
		}

		var cartOrder = (await unitOfWork.OrderRepository.GetAllAsync(o => o.Status == OrderStatus.Open, includeProperties: "OrderDetails")).SingleOrDefault();

		if (cartOrder == null)
		{
			logger.LogWarning("Cart is empty");
			return;
		}

		cartOrder.OrderDetails = cartOrder.OrderDetails.Where(cartOrder => cartOrder.ProductId != game.Id).ToList();

		// If cart is empty delete order
		if (cartOrder.OrderDetails.Count == 0)
		{
			logger.LogInformation("Cart is empty, deleting order");
			await unitOfWork.OrderRepository.DeleteByIdAsync(cartOrder.Id);
		}
		else
		{
			logger.LogInformation("Updating cart");
			unitOfWork.OrderRepository.Update(cartOrder);
		}
		await unitOfWork.SaveAsync();
	}

	public Task UpdateAsync(OrderModel model)
	{
		throw new NotImplementedException();
	}

	public Task<PaymentMethodsModel> GetPaymentMethodsAsync()
	{
		return Task.FromResult(new PaymentMethodsModel());
	}

	public async Task<IActionResult> ProcessPaymentAsync(string method, dynamic model = null)
	{
		logger.LogInformation("Processing payment with method {Method}", method);

		var cartOrder = (await unitOfWork.OrderRepository.GetAllAsync(o => o.Status == OrderStatus.Open, includeProperties: "OrderDetails")).SingleOrDefault();

		if (cartOrder == null)
		{
			logger.LogWarning("No open cart found for payment");
			throw new GameStoreValidationException("No open cart found for payment");
		}

		double sum = cartOrder.OrderDetails.Sum(od => (od.Price - od.Discount) * od.Quantity);

		// check if unitsinStock is higher than quantity

		switch (method)
		{
			case "Bank":
				cartOrder.Status = OrderStatus.Paid;
				unitOfWork.OrderRepository.Update(cartOrder);
				await unitOfWork.SaveAsync();
				return GenerateInvoice(Guid.Parse("00000000-0000-0000-0000-000000000000"), cartOrder.Id, sum);

			default:
				logger.LogWarning("Unknown payment method {Method}", method);
				throw new GameStoreValidationException("Unknown payment method");
		}
	}

	private FileContentResult GenerateInvoice(Guid userId, Guid orderId, double sum)
	{
		logger.LogInformation("Generating PDF invoice for Bank payment");

		var pdfBytes = GenerateInvoicePdf
			(userId,
			orderId, 
			DateTime.Now, 
			DateTime.Now.AddDays(7),
			sum);

		return new FileContentResult(pdfBytes, "application/pdf")
		{
			FileDownloadName = $"Invoice_{orderId}.pdf"
		};
	}

	public static byte[] GenerateInvoicePdf(Guid userId, Guid orderId, DateTime createdDate, DateTime validUntil, double sum)
	{
		var pdf = Document.Create(container =>
		{
			container.Page(page =>
			{
				page.Size(PageSizes.A4);
				page.Margin(50);
				page.DefaultTextStyle(x => x.FontSize(14));

				page.Content()
					.Column(column =>
					{
						column.Item().Text($"Invoice").FontSize(24).Bold();
						column.Item().Text($"User ID: {userId}");
						column.Item().Text($"Order ID: {orderId}");
						column.Item().Text($"Created: {createdDate:yyyy-MM-dd}");
						column.Item().Text($"Valid Until: {validUntil:yyyy-MM-dd}");
						column.Item().Text($"Sum: {sum:C}");
					});
			});
		});

		return pdf.GeneratePdf();
	}
}
