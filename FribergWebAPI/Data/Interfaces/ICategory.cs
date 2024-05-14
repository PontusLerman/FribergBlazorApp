using FribergWebAPI.Models;

//author: Christian Alp
namespace FribergWebAPI.Data.Interfaces
{
	public interface ICategory
	{
		Task<IEnumerable<Category>> GetAll();
		Task<Category> GetById(int id);
		Task Add(Category category);
		Task Update(Category category);
		Task Delete(int id);
	}
}