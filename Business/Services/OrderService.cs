using System.Net.Http.Json;
using System.Text.Json;
using AutoMapper;
using Business.Exceptions;
using Business.Interfaces;
using Business.Models;
using Common.Filters;
using Common.Options;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Business.Services;

public class OrderService(IUnitOfWork unitOfWork, IHttpClientFactory httpClientFactory, IMapper mapper, ILogger<OrderService> logger) : IOrderService
{
	public async Task AddToCartAsync(string key, Guid Id)
	{
		logger.LogInformation("Adding to cart with key {Key}", key);
		
		var game = (await unitOfWork.GameRepository.GetAllAsync(g => g.Key == key)).SingleOrDefault();

		if (game == null)
		{
			logger.LogWarning("Game with key {Key} not found", key);
			return;
		}

		var cartOrder = (await unitOfWork.OrderRepository.GetAllAsync(o => o.Status == OrderStatus.Open && o.CustomerId == Id, includeProperties: "OrderDetails")).SingleOrDefault();

		if (cartOrder == null)
		{
			logger.LogInformation("Creating new order");
			cartOrder = new Order
			{
				CustomerId = Id,
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

	public async Task AddGameToAnyCartAsync(Guid orderId, string key)
	{
		logger.LogInformation("Adding game to any cart with key {Key}", key);
		var game = (await unitOfWork.GameRepository.GetAllAsync(g => g.Key == key)).SingleOrDefault();
		if (game == null)
		{
			logger.LogWarning("Game with key {Key} not found", key);
			return;
		}
		var order = (await unitOfWork.OrderRepository.GetAllAsync(o => o.Id == orderId, includeProperties: "OrderDetails")).SingleOrDefault();
		if (order == null)
		{
			logger.LogWarning("Order with id {OrderId} not found", orderId);
			return;
		}

		foreach (var item in order.OrderDetails)
		{
			if (item.ProductId == game.Id)
			{
				logger.LogInformation("Incrementing quantity of product with id {ProductId}", game.Id);
				item.Quantity++;
				unitOfWork.OrderRepository.Update(order);
				await unitOfWork.SaveAsync();
				return;
			}
		}

		// Add new product to cart
		logger.LogInformation("Adding new product to cart with id {ProductId}", game.Id);
		var orderDetail = new OrderGame { OrderId = order.Id, ProductId = game.Id, Quantity = 1, Price = game.Price, Discount = game.Discount };
		await unitOfWork.OrderDetailsRepository.AddAsync(orderDetail);
		await unitOfWork.SaveAsync();
		logger.LogInformation("Game added to cart with id {ProductId}", game.Id);
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

	public async Task<IEnumerable<OrderModel>> GetPaidAndCancelledOrdersAsync(OrderHistoryFilter historyFilter)
	{
		logger.LogInformation("Getting paid and cancelled orders");
		var startDate = OrderHistoryFilter.ParseDate(historyFilter.Start) ?? DateTime.MinValue;
		var endDate = OrderHistoryFilter.ParseDate(historyFilter.End) ?? DateTime.MaxValue;
		var orders = await unitOfWork.OrderRepository.GetAllAsync(o => (o.Status == OrderStatus.Paid || o.Status == OrderStatus.Cancelled) && o.Date <= endDate && o.Date >= startDate, includeProperties: "OrderDetails");
		return mapper.Map<IEnumerable<OrderModel>>(orders);
	}

	public async Task<IEnumerable<OrderDetailsModel>> GetCartAsync(Guid Id)
	{
		logger.LogInformation("Getting open orders");
		var order = (await unitOfWork.OrderRepository.GetAllAsync(o => o.Status == OrderStatus.Open && o.CustomerId == Id, includeProperties: "OrderDetails")).SingleOrDefault();
		if(order is null)
		{
			return [];
		}
		
		return mapper.Map<IEnumerable<OrderDetailsModel>>(order.OrderDetails);
	}

	public async Task<IEnumerable<OrderModel>> GetOpenOrdersAsync()
	{
		var orders = await unitOfWork.OrderRepository.GetAllAsync(o => o.Status == OrderStatus.Open, includeProperties: "OrderDetails");
		if (orders is null)
		{
			return [];
		}

		return mapper.Map<IEnumerable<OrderModel>>(orders);
	}

	public async Task RemoveOrderDetailAsync(Guid orderId, Guid productId)
	{
		logger.LogInformation("Removing order detail with order id {OrderId} and product id {ProductId}", orderId, productId);
		var orderDetail = await unitOfWork.OrderDetailsRepository.GetByIDAsync(orderId, productId);
		if (orderDetail == null)
		{
			logger.LogWarning("Order detail with order id {OrderId} and product id {ProductId} not found", orderId, productId);
			return;
		}
		await unitOfWork.OrderDetailsRepository.DeleteByIdAsync(orderDetail.OrderId, orderDetail.ProductId);
		await unitOfWork.SaveAsync();
	}

	public async Task RemoveFromCartAsync(string key, Guid Id)
	{
		logger.LogInformation("Removing from cart with key {Key}", key);

		var game = (await unitOfWork.GameRepository.GetAllAsync(g => g.Key == key)).SingleOrDefault();

		if (game == null)
		{
			logger.LogWarning("Game with key {Key} not found", key);
			return;
		}

		var cartOrder = (await unitOfWork.OrderRepository.GetAllAsync(o => o.Status == OrderStatus.Open && o.CustomerId == Id, includeProperties: "OrderDetails")).SingleOrDefault();

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

	public Task<PaymentOptions> GetPaymentMethodsAsync()
	{
		return Task.FromResult(new PaymentOptions());
	}

	public async Task UpdateOrderDetailQuantityAsync(Guid orderId, Guid productId, int quantity)
	{
		logger.LogInformation("Updating order detail quantity with order id {OrderId} and product id {ProductId}", orderId, productId);
		var orderDetail = await unitOfWork.OrderDetailsRepository.GetByIDAsync(orderId, productId);
		if (orderDetail == null)
		{
			logger.LogWarning("Order detail with order id {OrderId} and product id {ProductId} not found", orderId, productId);
		}
		orderDetail.Quantity = quantity;
		unitOfWork.OrderDetailsRepository.Update(orderDetail);
		await unitOfWork.SaveAsync();
	}

	public async Task<IActionResult> ProcessPaymentAsync(string method, JsonElement model)
	{
		logger.LogInformation("Processing payment with method {Method}", method);

		var cartOrder = (await unitOfWork.OrderRepository.GetAllAsync(o => o.Status == OrderStatus.Open, includeProperties: "OrderDetails")).SingleOrDefault();

		if (cartOrder == null)
		{
			logger.LogWarning("No open cart found for payment");
			throw new GameStoreValidationException("No open cart found for payment");
		}

		double sum = cartOrder.OrderDetails.Sum(od => (od.Price - (od.Price * od.Discount/100)) * od.Quantity);

		// check if unitsinStock is higher than quantity
		foreach (var item in cartOrder.OrderDetails)
		{
			var game = await unitOfWork.GameRepository.GetByIDAsync(item.ProductId);

			if(game.UnitInStock < item.Quantity)
			{
				var errorResponse = new
				{
					error = "Insufficient stock",
					gameId = game.Id,
					requestedQuantity = item.Quantity,
					availableStock = game.UnitInStock
				};

				return new BadRequestObjectResult(errorResponse);
			}
		}
		
		HttpResponseMessage? apiResponse;
		switch (method)
		{
			case "Bank":
				cartOrder.Status = OrderStatus.Paid;
				unitOfWork.OrderRepository.Update(cartOrder);
				await unitOfWork.SaveAsync();
				return GenerateInvoice(Guid.Parse("00000000-0000-0000-0000-000000000000"), cartOrder.Id, sum);

			case "Visa":
				apiResponse = await ProcessVisaPaymentAsync(model,sum);
				break;

			case "IBox terminal":
				var terminalRequest = new { transactionAmount = sum, accountNumber = "3fa85f64-5717-4562-b3fc-2c963f66afa6" };
				apiResponse = await ProcessIBoxPaymentAsync(terminalRequest);
				break;

			default:
				logger.LogWarning("Unknown payment method {Method}", method);
				throw new GameStoreValidationException("Unknown payment method");
		}

		if (!apiResponse.IsSuccessStatusCode)
		{
			logger.LogError("Payment failed with status code {StatusCode}", apiResponse.StatusCode);
		}
		else
		{
			cartOrder.Status = OrderStatus.Paid;
			unitOfWork.OrderRepository.Update(cartOrder);
			await unitOfWork.SaveAsync();
		}

		var content = await apiResponse.Content.ReadAsStringAsync();

		return new ContentResult
		{
			StatusCode = (int)apiResponse.StatusCode,
			Content = content,
			ContentType = "application/json"
		};
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

	public async Task<HttpResponseMessage> ProcessVisaPaymentAsync(JsonElement visaPayload, double sum)
	{
		object? visaRequest;
		try
		{
			string holder = visaPayload.GetProperty("holder").GetString();
			string cardNumber = visaPayload.GetProperty("cardNumber").GetString();
			int monthExpire = visaPayload.GetProperty("monthExpire").GetInt32();
			int yearExpire = visaPayload.GetProperty("yearExpire").GetInt32();
			int cvv2 = visaPayload.GetProperty("cvv2").GetInt32();

			// validation is done on payment gateway

			visaRequest = new 
			{
				transactionAmount = sum,
				cardHolderName = holder,
				cardNumber = cardNumber,
				expirationMonth = monthExpire,
				expirationYear = yearExpire,
				cvv = cvv2
			};
		}
		catch (Exception e) when (e is not GameStoreValidationException)
		{
			logger.LogError(e, "Visa payment failed");
			throw new GameStoreValidationException("Invalid Visa model format.");
		}

		var client = httpClientFactory.CreateClient("PaymentClient");
		var response = await client.PostAsJsonAsync("api/payments/visa", visaRequest);

		if (response.IsSuccessStatusCode)
		{
			logger.LogInformation("Visa payment processed successfully.");
		}
		else
		{
			logger.LogError("Visa payment failed with status code: {StatusCode}", response.StatusCode);
		}

		return response;
	}

	public async Task<HttpResponseMessage> ProcessIBoxPaymentAsync(object iboxPayload)
	{
		var client = httpClientFactory.CreateClient("PaymentClient");
		var response = await client.PostAsJsonAsync("api/payments/ibox", iboxPayload);

		if (response.IsSuccessStatusCode)
		{
			logger.LogInformation("IBox payment processed successfully.");
		}
		else
		{
			logger.LogError("IBox payment failed with status code: {StatusCode}", response.StatusCode);
		}

		return response;
	}
}
