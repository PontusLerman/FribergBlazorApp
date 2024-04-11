using FribergWebAPI.Models;

namespace FribergWebAPI.Data
{
    public class RealtorRepository : IRealtor
    {
        private readonly ApplicationDbContext applicationDbContext;

        public RealtorRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public Task AddAsync(Realtor realtor)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Realtor realtor)
        {
            throw new NotImplementedException();
        }

        public Task<Realtor> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Realtor realtor)
        {
            throw new NotImplementedException();
        }
    }
}
