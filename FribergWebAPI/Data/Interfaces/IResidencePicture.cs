using FribergWebAPI.Models;

namespace FribergWebAPI.Data.Interfaces
{
    public interface IResidencePicture //author: Johan
    {
        Task AddAsync(ResidencePicture residencePicture);
        Task DeleteAsync(ResidencePicture residencePicture);
        Task<IEnumerable<ResidencePicture>> GetAllAsync();
        Task<ResidencePicture> GetByIdAsync(int id);
        Task UpdateAsync(ResidencePicture residencePicture);
    }
}