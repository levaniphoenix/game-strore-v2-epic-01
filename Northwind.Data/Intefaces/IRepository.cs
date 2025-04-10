using System.Linq.Expressions;
using MongoDB.Bson;

namespace Northwind.Data.Intefaces;
public interface IRepository<T> where T : class
{
	Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);
	Task AddAsync(T entity);
	Task<T?> GetByIdAsync(ObjectId id);
	Task DeleteAsync(ObjectId id);
	Task UpdateAsync(T entity);
}
