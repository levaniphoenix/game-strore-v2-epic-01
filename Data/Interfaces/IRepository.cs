using System.Linq.Expressions;

namespace Data.Interfaces
{
	public interface IRepository<TEntity> where TEntity : class
	{
		Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "");

		Task<TEntity?> GetByIDAsync(object id, Expression<Func<TEntity, bool>>? filter = null);

		Task AddAsync(TEntity entity);

		void Delete(TEntity entityToDelete);

		Task DeleteByIdAsync(object id);

		void Update(TEntity entityToUpdate);

		Task<int> GetTotalCountAsync();
	}
}
