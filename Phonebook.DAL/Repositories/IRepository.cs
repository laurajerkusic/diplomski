using System.Linq.Expressions;

namespace Phonebook.DAL.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
	Task<TEntity?> GetByIdAsync(int id);
	IQueryable<TEntity> GetAll();
	Task<IEnumerable<TEntity>> GetAllAsync();
	Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

	Task AddAsync(TEntity entity);
	Task AddRangeAsync(IEnumerable<TEntity> entities);

	Task UpdateAsync(TEntity entity);
	void Remove(TEntity entity);
	void RemoveRange(IEnumerable<TEntity> entities);

	Task<int> SaveChangesAsync();
}