using AutoMapper;
using Business.Interfaces.Northwind;
using Business.Models.Northwind;
using MongoDB.Bson;
using Northwind.Data.Entities;
using Northwind.Data.Intefaces;

namespace Business.Services.Northwind;

public class ProductService(IUnitOfWorkMongoDB unitOfWork, IMapper mapper) : IProductService
{
	public async Task AddAsync(ProductModel model)
	{
		await unitOfWork.Products.AddAsync(mapper.Map<Product>(model));
	}

	public async Task DeleteAsync(object modelId)
	{
		await unitOfWork.Products.DeleteAsync(ObjectId.Parse((string)modelId));
	}

	public async Task<IEnumerable<ProductModel>> GetAllAsync()
	{
		var products = await unitOfWork.Products.GetAll();
		return mapper.Map<IEnumerable<ProductModel>>(products);
	}

	public async Task<ProductModel?> GetByIdAsync(object id)
	{
		var product = await unitOfWork.Products.GetByIdAsync(ObjectId.Parse((string)id));
		var supplier = await unitOfWork.Suppliers.GetAll(x=> x.SupplierID == product.SupplierID);
		var category = await unitOfWork.Categories.GetAll(x => x.CategoryID == product.CategoryID);

		var productModel = mapper.Map<ProductModel>(product);
		productModel.Supplier = mapper.Map<SupplierModel>(supplier.FirstOrDefault());
		productModel.Category = mapper.Map<CategoryModel>(category.FirstOrDefault());
		return productModel;
	}

	public async Task UpdateAsync(ProductModel model)
	{
		await unitOfWork.Products.UpdateAsync(mapper.Map<Product>(model));
	}
}
