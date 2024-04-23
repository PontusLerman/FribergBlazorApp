using FribergWebAPI.Models;

namespace FribergWebAPI.Data.Interfaces
{
    public interface ICategory
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
    }
}