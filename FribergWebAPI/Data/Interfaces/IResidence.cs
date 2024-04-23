using FribergWebAPI.Models;

//author: Christian
namespace FribergWebAPI.Data.Interfaces
{
    public interface IResidence
    {
        Task<IEnumerable<Residence>> GetAll();
        Task<Residence> GetById(int id);
        Task Add(Residence residence);
        Task Update(Residence residence);
        Task Delete(int id);
    }
}