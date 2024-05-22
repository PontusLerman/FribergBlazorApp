using FribergWebAPI.Models;

//author: Pontus Lerman
namespace FribergWebAPI.Data.Interfaces
{
	public interface IRealtor
	{
		Task<Realtor> GetByIdAsync(int id);
		Task<IEnumerable<Realtor>> GetAllAsync();
		Task AddAsync(Realtor realtor);
		Task DeleteAsync(Realtor realtor);
		Task UpdateAsync(Realtor realtor);
	}
}
