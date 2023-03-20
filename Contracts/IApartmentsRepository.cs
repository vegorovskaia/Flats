using Entities.Helpers;
using Entities.Models;

namespace Contracts
{
	public interface IApartmentsRepository : IRepositoryBase<Apartments>
	{
		PagedList<Apartments> GetApartments(ApartmentParameters ownerParameters);

		Apartments GetApartmentById(int id);
		void UpdateApartment( Apartments apart);
		void DeleteApartment(Apartments apart);
	}
}
