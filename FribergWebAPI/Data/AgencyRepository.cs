using FribergWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FribergWebAPI.Data
{
    public class AgencyRepository : IAgency //author: Johan Krångh
    {
        private readonly FribergAPIContext fribergAPIContext;

        public AgencyRepository(FribergAPIContext fribergAPIContext)
        {
            this.fribergAPIContext = fribergAPIContext;
        }

        public async Task AddAsync(Agency agency)
        {
            await fribergAPIContext.Agency.AddAsync(agency);
            await fribergAPIContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Agency agency)
        {
            fribergAPIContext.Agency.Remove(agency);
            await fribergAPIContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Agency>> GetAllAsync()
        {
            return await fribergAPIContext.Agency.ToListAsync();
        }

        public async Task<Agency> GetByIdAsync(int id)
        {
            return await fribergAPIContext.Agency.FirstOrDefaultAsync(x => x.AgencyId == id);
        }

        public async Task UpdateAsync(Agency agency)
        {
            fribergAPIContext.Update(agency);
            await fribergAPIContext.SaveChangesAsync();
        }
    }
}
