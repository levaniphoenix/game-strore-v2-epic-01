using AutoMapper;
using Business.Interfaces.Northwind;
using Business.Models.Northwind;
using MongoDB.Bson;
using Northwind.Data.Entities;
using Northwind.Data.Intefaces;

namespace Business.Services.Northwind;

public class ShipperService(IUnitOfWorkMongoDB unitOfWork, IMapper mapper) : IShipperService
{
	public async Task AddAsync(ShipperModel model)
	{
		await unitOfWork.Shippers.AddAsync(mapper.Map<Shipper>(model));
	}
	public async Task DeleteAsync(object modelId)
	{
		await unitOfWork.Shippers.DeleteAsync(ObjectId.Parse((string)modelId));
	}
	public async Task<IEnumerable<ShipperModel>> GetAllAsync()
	{
		var shippers = await unitOfWork.Shippers.GetAll();
		return mapper.Map<IEnumerable<ShipperModel>>(shippers);
	}
	public async Task<ShipperModel?> GetByIdAsync(object id)
	{
		var shipper = await unitOfWork.Shippers.GetByIdAsync(ObjectId.Parse((string)id));
		return mapper.Map<ShipperModel>(shipper);
	}
	public async Task UpdateAsync(ShipperModel model)
	{
		await unitOfWork.Shippers.UpdateAsync(mapper.Map<Shipper>(model));
	}
}
