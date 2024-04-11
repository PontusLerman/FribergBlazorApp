using FribergWebAPI.Models;

namespace FribergWebAPI.Data
{
    public interface IRealtor
    {
        Task<Realtor> GetByIdAsync(int id);
        Task AddAsync(Realtor realtor);
        Task DeleteAsync(Realtor realtor);
        Task UpdateAsync(Realtor realtor);
    }
}
