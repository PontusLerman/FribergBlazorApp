using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FribergWebAPI.Models;

namespace FribergWebAPI.Data
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