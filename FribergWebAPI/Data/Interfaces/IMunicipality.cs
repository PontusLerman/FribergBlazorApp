using FribergWebAPI.Models;

//author: Johan Krångh
namespace FribergWebAPI.Data.Interfaces
{
	public interface IMunicipality 
	{
		Task AddAsync(Municipality municipality);
		Task DeleteAsync(Municipality municipality);
		Task<IEnumerable<Municipality>> GetAllAsync();
		Task<Municipality> GetByIdAsync(int id);
		Task UpdateAsync(Municipality municipality);
	}
}