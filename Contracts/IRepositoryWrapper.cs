namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IApartmentsRepository Apartments { get; }
       
		void Save();
	}
}
