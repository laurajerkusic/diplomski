using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Phonebook.DAL.Data;

namespace Phonebook.DAL.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
	protected readonly PhonebookDbContext _context;
	protected readonly DbSet<TEntity> _dbSet;

	public Repository(PhonebookDbContext context)
	{
		_context = context;
		_dbSet = context.Set<TEntity>();
	}

	public async Task<TEntity?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

	public IQueryable<TEntity> GetAll() => _dbSet;

	public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();

	public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
		=> await _dbSet.Where(predicate).ToListAsync();

	public async Task AddAsync(TEntity entity) => await _dbSet.AddAsync(entity);

	public async Task AddRangeAsync(IEnumerable<TEntity> entities)
		=> await _dbSet.AddRangeAsync(entities);

	public async Task UpdateAsync(TEntity entity)
	{
		_dbSet.Update(entity);
	}

	public void Remove(TEntity entity) => _dbSet.Remove(entity);

	public void RemoveRange(IEnumerable<TEntity> entities) => _dbSet.RemoveRange(entities);

	public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
}