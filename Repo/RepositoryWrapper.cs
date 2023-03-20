using Contracts;
using Entities;
using Entities.Helpers;
using Entities.Models;

namespace Repo
{
	public class RepositoryWrapper : IRepositoryWrapper
	{
		private flatsdbContext _repoContext;
		private IApartmentsRepository _apart;
		
		public IApartmentsRepository Apartments
		{
			get
			{
				if (_apart == null)
				{
					_apart = new ApartmentsRepository(_repoContext );
				}

				return _apart;
			}
		}

		
		public RepositoryWrapper(flatsdbContext repositoryContext )
		{
			_repoContext = repositoryContext;
		}

		public void Save()
		{
			_repoContext.SaveChanges();
		}
	}
}
