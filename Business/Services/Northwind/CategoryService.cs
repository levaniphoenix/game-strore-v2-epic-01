using AutoMapper;
using Business.Interfaces.Northwind;
using Business.Models.Northwind;
using MongoDB.Bson;
using Northwind.Data.Entities;
using Northwind.Data.Intefaces;

namespace Business.Services.Northwind;
public class CategoryService(IUnitOfWorkMongoDB unitOfWork,IMapper mapper) : ICategoryService
{
	public async Task AddAsync(CategoryModel model)
	{
		await unitOfWork.Categories.AddAsync(mapper.Map<Category>(model));
	}

	public async Task DeleteAsync(object modelId)
	{
		await unitOfWork.Categories.DeleteAsync(ObjectId.Parse((string)modelId));
	}

	public async Task<IEnumerable<CategoryModel>> GetAllAsync()
	{
		var categories = await unitOfWork.Categories.GetAll();
		return mapper.Map<IEnumerable<CategoryModel>>(categories);
	}

	public async Task<CategoryModel?> GetByIdAsync(object id)
	{
		var category = await unitOfWork.Categories.GetByIdAsync(ObjectId.Parse((string)id));
		return mapper.Map<CategoryModel>(category);
	}

	public async Task UpdateAsync(CategoryModel model)
	{
		await unitOfWork.Categories.UpdateAsync(mapper.Map<Category>(model));
	}
}
