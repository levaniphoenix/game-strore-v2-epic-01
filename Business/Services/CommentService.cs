using AutoMapper;
using Business.Exceptions;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Microsoft.Extensions.Logging;

namespace Business.Services;
public class CommentService(IUserService userService,IUnitOfWork unitOfWork, IMapper mapper, ILogger<CommentService> logger) : ICommentService
{
	public async Task AddAsync(CommentModel model)
	{
		logger.LogInformation("checking if user:{Username} is banned", model.Comment.Name);

		if (await userService.IsUserBanned(model.Comment.Name))
		{
			logger.LogWarning("User {Username} is banned", model.Comment.Name);
			throw new GameStoreValidationException("User is banned");
		}

		logger.LogInformation("Adding a new comment");
		var comment = mapper.Map<Comment>(model);
		await unitOfWork.CommentRepository.AddAsync(comment);
		await unitOfWork.SaveAsync();
		logger.LogInformation("Comment added successfully");
	}

	public async Task DeleteAsync(object modelId)
	{
		logger.LogInformation("Deleting comment with ID: {CommentId}", modelId);
		await unitOfWork.CommentRepository.DeleteByIdAsync(modelId);
		await unitOfWork.SaveAsync();
		logger.LogInformation("Comment with ID {CommentId} deleted successfully", modelId);
	}

	public async Task<IEnumerable<CommentModel>> GetAllAsync()
	{
		logger.LogInformation("Fetching all comments");
		var comments = await unitOfWork.CommentRepository.GetAllAsync();
		logger.LogInformation("All comments fetched successfully");
		return mapper.Map<IEnumerable<CommentModel>>(comments);
	}

	public async Task<CommentModel?> GetByIdAsync(object id)
	{
		logger.LogInformation("Fetching comment with ID: {CommentId}", id);
		var comment = await unitOfWork.CommentRepository.GetByIDAsync(id);
		
		if (comment == null)
		{
			logger.LogWarning("Comment with ID {CommentId} not found", id);
			return null;
		}

		logger.LogInformation("Comment with ID {CommentId} fetched successfully", id);
		return mapper.Map<CommentModel?>(comment);
	}

	public async Task<IEnumerable<CommentModel>> GetCommentsByGameKeyAsync(string key)
	{
		logger.LogInformation("Fetching comments for game with key: {GameKey}", key);
		var comments = await unitOfWork.CommentRepository.GetAllAsync(c => c.Game.Key == key);

		comments = comments.Where(x => x.ParentId == null);

		if (comments == null)
		{
			logger.LogWarning("No comments found for game with key {GameKey}", key);
			return [];
		}
		logger.LogInformation("Comments for game with key {GameKey} fetched successfully", key);
		return mapper.Map<IEnumerable<CommentModel>>(comments);
	}

	public async Task QuoteCommentAsync(Guid? parentCommentId, string name, string quoteBody)
	{
		logger.LogInformation("checking if user:{Username} is banned", name);

		if (await userService.IsUserBanned(name))
		{
			logger.LogWarning("User {Username} is banned", name);
			throw new GameStoreValidationException("User is banned");
		}

		logger.LogInformation("Quoting comment with ID: {CommentId}", parentCommentId);

		if (parentCommentId == null)
		{
			logger.LogWarning("Parent comment ID is null");
			throw new GameStoreValidationException("Parent Id is null");
		}

		var parentComment = await unitOfWork.CommentRepository.GetByIDAsync(parentCommentId);

		if (parentComment == null)
		{
			logger.LogWarning("Comment with ID {CommentId} not found", parentCommentId);
			return;
		}

		var quote = new Comment
		{
			Id = Guid.NewGuid(),
			Name = name,
			Body = $"[{parentComment.Body}], {quoteBody}",
			ParentId = parentCommentId,
			GameId = parentComment.GameId
		};

		await unitOfWork.CommentRepository.AddAsync(quote);
		await unitOfWork.SaveAsync();
		logger.LogInformation("Comment with ID {CommentId} quoted successfully", parentCommentId);
	}

	public async Task ReplyToCommentAsync(Guid? parentCommentId, string name, string replyBody)
	{
		logger.LogInformation("checking if user:{Username} is banned", name);

		if (await userService.IsUserBanned(name))
		{
			logger.LogWarning("User {Username} is banned", name);
			throw new GameStoreValidationException("User is banned");
		}

		logger.LogInformation("Replying to comment with ID: {CommentId}", parentCommentId);

		if (parentCommentId == null)
		{
			logger.LogWarning("Parent comment ID is null");
			throw new GameStoreValidationException("Parent Id is null");
		}

		var parentComment = await unitOfWork.CommentRepository.GetByIDAsync(parentCommentId);

		if (parentComment == null)
		{
			logger.LogWarning("Comment with ID {CommentId} not found", parentCommentId);
			return;
		}

		var reply = new Comment
		{
			Id = Guid.NewGuid(),
			Name = name,
			Body = $"[{parentComment.Name}], {replyBody}",
			ParentId = parentCommentId,
			GameId = parentComment.GameId
		};

		await unitOfWork.CommentRepository.AddAsync(reply);
		await unitOfWork.SaveAsync();
		logger.LogInformation("Comment with ID {CommentId} replied to successfully", parentCommentId);
	}

	public async Task UpdateAsync(CommentModel model)
	{
		var comment = mapper.Map<Comment>(model);
		logger.LogInformation("Updating comment with ID: {CommentId}", comment.Id);
		unitOfWork.CommentRepository.Update(comment);
		await unitOfWork.SaveAsync();
		logger.LogInformation("Comment with ID {CommentId} updated successfully", comment.Id);
	}

	public async Task BanUserAsync(BanRequestModel banRequest)
	{
		logger.LogInformation("Banning user with Name: {UserName}", banRequest.UserName);
		var comments = await unitOfWork.CommentRepository.GetAllAsync(c => c.Name == banRequest.UserName);
		
		if (comments == null)
		{
			logger.LogWarning("No comments found for user with name {UserName}", banRequest.UserName);
			throw new GameStoreValidationException("No comments found for user");
		}

		await userService.BanUser(banRequest);

		foreach (var comment in comments)
		{
			comment.IsDeleted = true;
			unitOfWork.CommentRepository.Update(comment);
		}
		await unitOfWork.SaveAsync();
		logger.LogInformation("User with name {UserName} banned successfully", banRequest.UserName);
	}
}
