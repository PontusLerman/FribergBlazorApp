using FribergWebAPI.Data.Interfaces;
using FribergWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FribergWebAPI.Data.Repositories
{
    //Pontus
    public class RealtorRepository : IRealtor
    {
        private readonly ApplicationDbContext applicationDbContext;

        public RealtorRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task AddAsync(Realtor realtor)
        {
            await applicationDbContext.AddAsync(realtor);
            applicationDbContext.Entry(realtor.Agency).State = EntityState.Unchanged;
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Realtor realtor)
        {
            applicationDbContext.Remove(realtor);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Realtor>> GetAllAsync()
        {
            return await applicationDbContext.Realtors.Include(x => x.Agency).Include(x => x.ResidenceList).OrderBy(r => r.FirstName).ToListAsync();
        }

        public async Task<Realtor> GetByIdAsync(int id)
        {
            return await applicationDbContext.Realtors.Include(x => x.Agency).Include(x => x.ResidenceList).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Realtor realtor)
        {
            applicationDbContext.Update(realtor);
            await applicationDbContext.SaveChangesAsync();
        }
    }
}
