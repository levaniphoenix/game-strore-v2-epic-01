using Data.Data;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories
{
	public class GenericRepository<TEntity>: IRepository<TEntity> where TEntity: class
	{
		internal GamestoreDBContext context;
		internal DbSet<TEntity> dbSet;

		public GenericRepository(GamestoreDBContext context)
		{
			this.context = context;
			this.dbSet = context.Set<TEntity>();
		}

		public async virtual Task<IEnumerable<TEntity>> GetAllAsync(
			Expression<Func<TEntity, bool>>? filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
			string includeProperties = "")
		{
			IQueryable<TEntity> query = dbSet;

			if (filter != null)
			{
				query = query.Where(filter);
			}

			foreach (var includeProperty in includeProperties.Split
				(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}

			if (orderBy != null)
			{
				return await orderBy(query).ToListAsync();
			}
			else
			{
				return await query.ToListAsync();
			}
		}

		public async virtual Task<TEntity?> GetByIDAsync(object id)
		{
			return await dbSet.FindAsync(id);
		}

		public async virtual Task AddAsync(TEntity entity)
		{
			await dbSet.AddAsync(entity);
		}

		public async virtual Task DeleteByIdAsync(object id)
		{
			TEntity? entityToDelete = await dbSet.FindAsync(id);
			if (entityToDelete != null)
			{
				Delete(entityToDelete);
			}
		}

		public virtual void Delete(TEntity entityToDelete)
		{
			if (context.Entry(entityToDelete).State == EntityState.Detached)
			{
				dbSet.Attach(entityToDelete);
			}
			dbSet.Remove(entityToDelete);
		}

		public virtual void Update(TEntity entityToUpdate)
		{
			dbSet.Attach(entityToUpdate);
			context.Entry(entityToUpdate).State = EntityState.Modified;
		}
	}
}
