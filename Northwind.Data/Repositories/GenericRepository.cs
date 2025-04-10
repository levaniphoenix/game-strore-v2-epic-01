using System.Linq.Expressions;
using Northwind.Data.Intefaces;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Northwind.Data.Repositories;

public class GenericRepository<T> : IRepository<T> where T : class
{
	private readonly IMongoCollection<T> collection;

	public GenericRepository(IMongoDatabase database, string collectionName)
	{
		collection = database.GetCollection<T>(collectionName);
	}

	public async Task AddAsync(T entity)
	{
		await collection.InsertOneAsync(entity);
	}

	public async Task DeleteAsync(ObjectId id)
	{
		await collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id));
	}

	public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
	{
		var query = collection.AsQueryable();

		if (filter != null)
		{
			query = query.Where(filter);
		}

		if (orderBy != null)
		{
			query = orderBy(query);
		}

		return await query.ToListAsync();
	}

	public async Task<T?> GetByIdAsync(ObjectId id)
	{
		return await collection.Find(Builders<T>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
	}

	public async Task UpdateAsync(T entity)
	{
		await collection.ReplaceOneAsync(Builders<T>.Filter.Eq("Id", entity.GetType().GetProperty("Id")?.GetValue(entity)), entity);
	}
}
