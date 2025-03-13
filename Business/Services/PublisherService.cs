using AutoMapper;
using Business.Exceptions;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Microsoft.Extensions.Logging;

namespace Business.Services;

public class PublisherService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PublisherService> logger) : IPublisherService
{
	public async Task AddAsync(PublisherModel model)
	{
		logger.LogInformation("Adding a new publisher: {PublisherCompanyName}", model.Publisher.CompanyName);
		
		await ValidatePublisher(model);

		var publisher = mapper.Map<Publisher>(model);
		await unitOfWork.PublisherRepository!.AddAsync(publisher);
		await unitOfWork.SaveAsync();

		logger.LogInformation("Publisher {PublisherCompanyName} added successfully", model.Publisher.CompanyName);
	}

	public async Task DeleteAsync(object modelId)
	{
		logger.LogInformation("Deleting publisher with ID: {PublisherId}", modelId);

		await unitOfWork.PlatformRepository!.DeleteByIdAsync(modelId);
		await unitOfWork.SaveAsync();

		logger.LogInformation("Publisher with ID {PublisherId} deleted successfully", modelId);
	}

	public async Task<IEnumerable<PublisherModel>> GetAllAsync()
	{
		logger.LogInformation("Fetching all publishers");

		var publishers = await unitOfWork.PublisherRepository!.GetAllAsync();
		logger.LogInformation("Fetched {Count} publishers", publishers.Count());

		return mapper.Map<IEnumerable<PublisherModel>>(publishers);
	}

	public async Task<PublisherModel?> GetByIdAsync(object id)
	{
		logger.LogInformation("Fetching publisher by ID: {PublisherId}", id);

		var publisher = await unitOfWork.PublisherRepository!.GetByIDAsync(id);
		logger.LogInformation("Fetched publisher by ID: {PublisherId}, Exists: {Exists}", id, publisher is not null);

		return mapper.Map<PublisherModel?>(publisher);
	}

	public async Task<IEnumerable<GameModel?>> GetGamesByPublisherIdAsync(Guid id)
	{
		logger.LogInformation("Fetching games for publisher ID: {PublisherId}", id);
		
		var games = await unitOfWork.GameRepository!.GetAllAsync(g => g.PublisherId == id);
		logger.LogInformation("Fetched {Count} games for publisher ID: {PublisherId}", games.Count(), id);
		
		return mapper.Map<IEnumerable<GameModel?>>(games);
	}

	public async Task UpdateAsync(PublisherModel model)
	{
		logger.LogInformation("Updating publisher with Name: {PublisherCompanyName}", model.Publisher.CompanyName);

		await ValidatePublisher(model);
		var publisher = mapper.Map<Publisher>(model);
		unitOfWork.PublisherRepository!.Update(publisher);
		await unitOfWork.SaveAsync();

		logger.LogInformation("Publisher with ID {PublisherCompanyName} updated successfully", model.Publisher.CompanyName);
	}

	private async Task ValidatePublisher(PublisherModel model)
	{
		logger.LogInformation("Validating publisher: {PublisherCompanyName}", model.Publisher.CompanyName);

		var publisher = await unitOfWork.PublisherRepository!.GetAllAsync(p => p.CompanyName == model.Publisher.CompanyName);
		if (publisher is not null)
		{
			logger.LogError("Publisher with name {PublisherCompanyName} already exists", model.Publisher.CompanyName);
			throw new GameStoreValidationException(ErrorMessages.PublisherNameAlreadyExists);
		}

		logger.LogInformation("Validation succesful for publisher: {PublisherCompanyName}", model.Publisher.CompanyName);
	}
}
