using FribergWebAPI.Models;

//author: Christian
namespace FribergWebAPI.Data.Interfaces
{
    public interface IResidence
    {
        Task<IEnumerable<Residence>> GetAllAsync();
        Task<Residence> GetByIdAsync(int id);
        Task AddAsync(Residence residence);
        Task UpdateAsync(Residence residence);
        Task DeleteAsync(Residence residence);
    }
}