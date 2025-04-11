using AutoMapper;
using Business.Exceptions;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class RoleService(IUnitOfWork unitOfWork, IMapper mapper) : IRoleService
{
	public async Task AddAsync(RoleModel model)
	{
		await ValidateRole(model);
		await unitOfWork.RoleRepository.AddAsync(mapper.Map<Role>(model));
		await unitOfWork.SaveAsync();
	}

	public async Task DeleteAsync(object modelId)
	{
		await unitOfWork.RoleRepository.DeleteByIdAsync(modelId);
	}

	public async Task<IEnumerable<RoleModel>> GetAllAsync()
	{
		var roles = await unitOfWork.RoleRepository.GetAllAsync();
		return mapper.Map<IEnumerable<RoleModel>>(roles);
	}

	public async Task<RoleModel?> GetByIdAsync(object id)
	{
		var role = await unitOfWork.RoleRepository.GetByIDAsync(id);
		return mapper.Map<RoleModel>(role);
	}

	public async Task UpdateAsync(RoleModel model)
	{
		await ValidateRole(model);
		unitOfWork.RoleRepository.Update(mapper.Map<Role>(model));
		await unitOfWork.SaveAsync();
	}

	public async Task ValidateRole(RoleModel model)
	{
		var role = (await unitOfWork.RoleRepository.GetAllAsync(x => x.Name == model.Name)).SingleOrDefault();

		if (role != null)
		{
			throw new GameStoreValidationException("Role with this name already exists");
		}
	}
}
