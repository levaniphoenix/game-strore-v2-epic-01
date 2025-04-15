using AutoMapper;
using Business.Interfaces.Northwind;
using Business.Models.Northwind;
using MongoDB.Bson;
using Northwind.Data.Entities;
using Northwind.Data.Intefaces;

namespace Business.Services.Northwind;

public class NorthwindOrderService(IUnitOfWorkMongoDB unitOfWork, IMapper mapper) : INorthwindOrderService
{
	public async Task AddAsync(OrderModel model)
	{
		await unitOfWork.Orders.AddAsync(mapper.Map<Order>(model));
	}
	public async Task DeleteAsync(object modelId)
	{
		await unitOfWork.Orders.DeleteAsync(ObjectId.Parse((string)modelId));
	}
	public async Task<IEnumerable<OrderModel>> GetAllAsync()
	{
		var orders = await unitOfWork.Orders.GetAll();
		return mapper.Map<IEnumerable<OrderModel>>(orders);
	}
	public async Task<OrderModel?> GetByIdAsync(object id)
	{
		var order = await unitOfWork.Orders.GetByIdAsync(ObjectId.Parse((string)id));
		return mapper.Map<OrderModel>(order);
	}
	public async Task UpdateAsync(OrderModel model)
	{
		await unitOfWork.Orders.UpdateAsync(mapper.Map<Order>(model));
	}
}
