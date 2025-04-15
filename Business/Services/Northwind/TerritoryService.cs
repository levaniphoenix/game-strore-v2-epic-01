using AutoMapper;
using Business.Interfaces.Northwind;
using Business.Models.Northwind;
using MongoDB.Bson;
using Northwind.Data.Entities;
using Northwind.Data.Intefaces;

namespace Business.Services.Northwind;

internal class TerritoryService(IUnitOfWorkMongoDB unitOfWork, IMapper mapper) : ITerritoryService
{
	public async Task AddAsync(TerritoryModel model)
	{
		await unitOfWork.Territories.AddAsync(mapper.Map<Territory>(model));
	}
	public async Task DeleteAsync(object modelId)
	{
		await unitOfWork.Territories.DeleteAsync(ObjectId.Parse((string)modelId));
	}
	public async Task<IEnumerable<TerritoryModel>> GetAllAsync()
	{
		var territories = await unitOfWork.Territories.GetAll();
		return mapper.Map<IEnumerable<TerritoryModel>>(territories);
	}
	public async Task<TerritoryModel?> GetByIdAsync(object id)
	{
		var territory = await unitOfWork.Territories.GetByIdAsync(ObjectId.Parse((string)id));
		return mapper.Map<TerritoryModel>(territory);
	}
	public async Task UpdateAsync(TerritoryModel model)
	{
		await unitOfWork.Territories.UpdateAsync(mapper.Map<Territory>(model));
	}
}
