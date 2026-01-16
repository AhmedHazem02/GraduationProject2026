using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_P2026.Infastructure.Repositories
{
	public interface IRepository<T>where T : class
	{
		Task<T> GetByIdAsync(Guid id);
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> AddAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(T entity);
		Task SaveChangesAsync();
		Task Rollback();
		Task Commit();
		IDbContextTransaction BeginTransaction();
		IQueryable<T> GetWithNoTracking();
		IQueryable<T> GetWithTracking();

	}
}
