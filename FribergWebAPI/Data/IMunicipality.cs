using FribergWebAPI.Models;

namespace FribergWebAPI.Data
{
    public interface IMunicipality //author: Johan
    {
        Task AddAsync(Municipality municipality);
        Task DeleteAsync(Municipality municipality);
        Task<IEnumerable<Municipality>> GetAllAsync();
        Task<Municipality> GetByIdAsync(int id);
        Task UpdateAsync(Municipality municipality);
    }
}