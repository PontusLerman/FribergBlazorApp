using FribergWebAPI.Models;

//author: Christian Alp, co-author: Johan Krångh
namespace FribergWebAPI.Data.Interfaces
{
    public interface IResidence
    {
        Task<IEnumerable<Residence>> GetAll();
        Task<IEnumerable<Residence>> GetAllByRealtorAsync(string realtorId);
        Task<IEnumerable<Residence>> GetAllByAgencyAsync(int agencyId);
        Task<Residence> GetById(int id);
        Task Add(Residence residence);
        Task Update(Residence residence);
        Task Delete(int id);
    }
}