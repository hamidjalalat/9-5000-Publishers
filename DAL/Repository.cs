using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL
{
	public class Repository<T> :
		object, IRepository<T> where T : Models.BaseEntity
	{
	

		public Repository(Models.DatabaseContext databaseContext)
		{
			if (databaseContext == null)
			{
				throw (new System.ArgumentNullException("databaseContext"));
			}

			DatabaseContext = databaseContext;
			DbSet = DatabaseContext.Set<T>();
			
		}

		protected DbSet<T> DbSet { get; set; }

		protected Models.DatabaseContext DatabaseContext { get; set; }

		public virtual void Insert(T entity)
		{
			if (entity == null)
			{
				throw (new System.ArgumentNullException("entity"));
			}

			DbSet.Add(entity);
		}

		public virtual void Update(T entity)
		{
			if (entity == null)
			{
				throw (new System.ArgumentNullException("entity"));
			}
			DbSet.Update(entity);
		
		}

		public virtual void Delete(T entity)
		{
			if (entity == null)
			{
				throw (new System.ArgumentNullException("entity"));
			}

			DbSet.Remove(entity);

		}

		public virtual T GetById(int id)
		{
			return (DbSet.Find(id));
		}

		public virtual bool DeleteById(int id)
		{
			T oEntity = GetById(id);

			if (oEntity == null)
			{
				return (false);
			}
			else
			{
				Delete(oEntity);

				return (true);
			}
		}

		public virtual IQueryable<T> Get()
		{
			return (DbSet);
		}

		public IQueryable<T> Get
			(System.Linq.Expressions.Expression<System.Func<T, bool>> predicate)
		{
			return (DbSet.Where(predicate));
		}

		
	}
}
