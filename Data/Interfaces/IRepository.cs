using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
	public interface IRepository<TEntity> where TEntity : class
	{
		Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "");

		Task<TEntity?> GetByIDAsync(object id);

		Task AddAsync(TEntity entity);

		void Delete(TEntity entityToDelete);

		Task DeleteByIdAsync(object id);

		void Update(TEntity entityToUpdate);
	}
}
