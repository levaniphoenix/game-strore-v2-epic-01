using AutoMapper;
using Business.Interfaces.Northwind;
using Business.Models.Northwind;
using MongoDB.Bson;
using Northwind.Data.Entities;
using Northwind.Data.Intefaces;

namespace Business.Services.Northwind;

internal class CustomerService(IUnitOfWorkMongoDB unitOfWork, IMapper mapper) : ICustomerService
{
	public async Task AddAsync(CustomerModel model)
	{
		await unitOfWork.Customers.AddAsync(mapper.Map<Customer>(model));
	}
	public async Task DeleteAsync(object modelId)
	{
		await unitOfWork.Customers.DeleteAsync(ObjectId.Parse((string)modelId));
	}
	public async Task<IEnumerable<CustomerModel>> GetAllAsync()
	{
		var customers = await unitOfWork.Customers.GetAll();
		return mapper.Map<IEnumerable<CustomerModel>>(customers);
	}
	public async Task<CustomerModel?> GetByIdAsync(object id)
	{
		var customer = await unitOfWork.Customers.GetByIdAsync(ObjectId.Parse((string)id));
		return mapper.Map<CustomerModel>(customer);
	}
	public async Task UpdateAsync(CustomerModel model)
	{
		await unitOfWork.Customers.UpdateAsync(mapper.Map<Customer>(model));
	}
}
