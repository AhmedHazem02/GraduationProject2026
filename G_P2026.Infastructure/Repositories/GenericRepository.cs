using G_P2026.Infastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_P2026.Infastructure.Repositories
{
	public class GenericRepository<T>:IRepository<T>where T : class
	{
		
		public GenericRepository() {
			 
		}

	 

		public Task<T> AddAsync(T entity)
		{
			throw new NotImplementedException();
		}

		public IDbContextTransaction BeginTransaction()
		{
			throw new NotImplementedException();
		}

		public Task Commit()
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(T entity)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<T>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<T> GetByIdAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public IQueryable<T> GetWithNoTracking()
		{
			throw new NotImplementedException();
		}

		public IQueryable<T> GetWithTracking()
		{
			throw new NotImplementedException();
		}

		public Task Rollback()
		{
			throw new NotImplementedException();
		}

		public Task SaveChangesAsync()
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(T entity)
		{
			throw new NotImplementedException();
		}
	}
}
