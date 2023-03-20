using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Repo
{
	public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
	{
		protected flatsdbContext RepositoryContext { get; set; }

		public RepositoryBase(flatsdbContext repositoryContext)
		{
			RepositoryContext = repositoryContext;
		}

		public IQueryable<T> FindAll()
		{
			return RepositoryContext.Set<T>()
				.AsNoTracking();
		}

		public IQueryable<T> FindByCondition(Expression <Func<T, bool>> expression)
		{
			return RepositoryContext.Set<T>()
				.Where(expression)
				.AsNoTracking();
		}

		
		public void Update(T entity)
		{
			RepositoryContext.Set<T>().Update(entity);
		}

		public void Delete(T entity)
		{
			RepositoryContext.Set<T>().Remove(entity);
		}
	}
}
