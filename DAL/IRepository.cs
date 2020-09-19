namespace DAL
{
	public interface IRepository<T> where T : Models.BaseEntity
	{
		void Insert(T entity);

		void Update(T entity);

		void Delete(T entity);

		T GetById(int id);

		bool DeleteById(int id);

		System.Linq.IQueryable<T> Get();

		System.Linq.IQueryable<T> Get
			(System.Linq.Expressions.Expression<System.Func<T, bool>> predicate);

	
	}
}
