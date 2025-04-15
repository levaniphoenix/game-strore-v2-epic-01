using AutoMapper;
using Business.Interfaces.Northwind;
using Business.Models.Northwind;
using MongoDB.Bson;
using Northwind.Data.Entities;
using Northwind.Data.Intefaces;

namespace Business.Services.Northwind;

public class EmployeeService(IUnitOfWorkMongoDB unitOfWork, IMapper mapper) : IEmployeeService
{
	public async Task AddAsync(EmployeeModel model)
	{
		await unitOfWork.Employees.AddAsync(mapper.Map<Employee>(model));
	}
	public async Task DeleteAsync(object modelId)
	{
		await unitOfWork.Employees.DeleteAsync(ObjectId.Parse((string)modelId));
	}
	public async Task<IEnumerable<EmployeeModel>> GetAllAsync()
	{
		var employees = await unitOfWork.Employees.GetAll();
		return mapper.Map<IEnumerable<EmployeeModel>>(employees);
	}
	public async Task<EmployeeModel?> GetByIdAsync(object id)
	{
		var employee = await unitOfWork.Employees.GetByIdAsync(ObjectId.Parse((string)id));
		return mapper.Map<EmployeeModel>(employee);
	}
	public async Task UpdateAsync(EmployeeModel model)
	{
		await unitOfWork.Employees.UpdateAsync(mapper.Map<Employee>(model));
	}
}
