using System.Linq.Expressions;
using Data.Data;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
	public abstract class GenericRepository<TEntity>: IRepository<TEntity> where TEntity : class
	{
		private readonly GamestoreDBContext context;

		public GamestoreDBContext Context => context;

		internal DbSet<TEntity> dbSet;

		protected GenericRepository(GamestoreDBContext context)
		{
			this.context = context;
			dbSet = context.Set<TEntity>();
		}

		public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
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

			return orderBy != null ? await orderBy(query).ToListAsync() : (IEnumerable<TEntity>)await query.ToListAsync();
		}

		public virtual async Task<TEntity?> GetByIDAsync(object id)
		{
			return await dbSet.FindAsync(id);
		}

		public virtual async Task AddAsync(TEntity entity)
		{
			await dbSet.AddAsync(entity);
		}

		public virtual async Task DeleteByIdAsync(object id)
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
