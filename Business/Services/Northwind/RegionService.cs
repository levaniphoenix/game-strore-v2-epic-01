using AutoMapper;
using Business.Interfaces.Northwind;
using Business.Models.Northwind;
using MongoDB.Bson;
using Northwind.Data.Entities;
using Northwind.Data.Intefaces;

namespace Business.Services.Northwind;
internal class RegionService(IUnitOfWorkMongoDB unitOfWork, IMapper mapper) : IRegionService
{
	public async Task AddAsync(RegionModel model)
	{
		await unitOfWork.Regions.AddAsync(mapper.Map<Region>(model));
	}
	public async Task DeleteAsync(object modelId)
	{
		await unitOfWork.Regions.DeleteAsync(ObjectId.Parse((string)modelId));
	}
	public async Task<IEnumerable<RegionModel>> GetAllAsync()
	{
		var regions = await unitOfWork.Regions.GetAll();
		return mapper.Map<IEnumerable<RegionModel>>(regions);
	}
	public async Task<RegionModel?> GetByIdAsync(object id)
	{
		var region = await unitOfWork.Regions.GetByIdAsync(ObjectId.Parse((string)id));
		return mapper.Map<RegionModel>(region);
	}
	public async Task UpdateAsync(RegionModel model)
	{
		await unitOfWork.Regions.UpdateAsync(mapper.Map<Region>(model));
	}
}
