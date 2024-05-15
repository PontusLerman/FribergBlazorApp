using FribergWebAPI.Models;

//author: Johan Krångh
namespace FribergWebAPI.Data.Interfaces
{
    public interface IAgency 
    {
        Task AddAsync(Agency agency);
        Task DeleteAsync(Agency agency);
        Task<IEnumerable<Agency>> GetAllAsync();
        Task<Agency> GetByIdAsync(int id);
        Task UpdateAsync(Agency agency);
    }
}