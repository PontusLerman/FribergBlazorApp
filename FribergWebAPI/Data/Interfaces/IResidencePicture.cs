using FribergWebAPI.Models;

//author: Johan Krångh
namespace FribergWebAPI.Data.Interfaces
{
	public interface IResidencePicture 
	{
		Task AddAsync(ResidencePicture residencePicture);
		Task DeleteAsync(ResidencePicture residencePicture);
		Task<IEnumerable<ResidencePicture>> GetAllAsync();
		Task<ResidencePicture> GetByIdAsync(int id);
		Task UpdateAsync(ResidencePicture residencePicture);
	}
}