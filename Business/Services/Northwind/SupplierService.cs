using AutoMapper;
using Business.Interfaces.Northwind;
using Business.Models.Northwind;
using MongoDB.Bson;
using Northwind.Data.Entities;
using Northwind.Data.Intefaces;

namespace Business.Services.Northwind;

internal class SupplierService(IUnitOfWorkMongoDB unitOfWork, IMapper mapper) : ISupplierService
{
	public async Task AddAsync(SupplierModel model)
	{
		await unitOfWork.Suppliers.AddAsync(mapper.Map<Supplier>(model));
	}
	public async Task DeleteAsync(object modelId)
	{
		await unitOfWork.Suppliers.DeleteAsync(ObjectId.Parse((string)modelId));
	}
	public async Task<IEnumerable<SupplierModel>> GetAllAsync()
	{
		var suppliers = await unitOfWork.Suppliers.GetAll();
		return mapper.Map<IEnumerable<SupplierModel>>(suppliers);
	}
	public async Task<SupplierModel?> GetByIdAsync(object id)
	{
		var supplier = await unitOfWork.Suppliers.GetByIdAsync(ObjectId.Parse((string)id));
		return mapper.Map<SupplierModel>(supplier);
	}
	public async Task UpdateAsync(SupplierModel model)
	{
		await unitOfWork.Suppliers.UpdateAsync(mapper.Map<Supplier>(model));
	}
}
