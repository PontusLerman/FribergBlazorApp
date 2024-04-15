using FribergWebAPI.Models;

namespace FribergWebAPI.Data
{
    public interface IAgency //author: Johan Krångh
    {
        Task AddAsync(Agency agency);
        Task DeleteAsync(Agency agency);
        Task<IEnumerable<Agency>> GetAllAsync();
        Task<Agency> GetByIdAsync(int id);
        Task UpdateAsync(Agency agency);
    }
}