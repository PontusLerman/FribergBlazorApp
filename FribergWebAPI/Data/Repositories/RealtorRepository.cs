using FribergWebAPI.Data.Interfaces;
using FribergWebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FribergWebAPI.Data.Repositories
{
    //Pontus
    public class RealtorRepository : IRealtor
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly UserManager<Realtor> userManager;

        public RealtorRepository(ApplicationDbContext applicationDbContext, UserManager<Realtor> userManager)
        {

            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
        }
        public async Task AddAsync(Realtor realtor)
        {
            var result = await userManager.CreateAsync(realtor);
            if (result.Succeeded)
            {
                await applicationDbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Failed to create user");
            }
            //await applicationDbContext.AddAsync(realtor);
            //applicationDbContext.Entry(realtor.Agency).State = EntityState.Unchanged;
            //await applicationDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Realtor realtor)
        {
            var result = await userManager.DeleteAsync(realtor);
            await applicationDbContext.SaveChangesAsync();
            if (!result.Succeeded)
            {
                throw new Exception("Failed to delete user");
            }
            //applicationDbContext.Remove(realtor);
        }

        public async Task<IEnumerable<Realtor>> GetAllAsync()
        {
            return await applicationDbContext.Realtors.Include(x => x.Agency).Include(x => x.ResidenceList).OrderBy(r => r.FirstName).ToListAsync();
        }

        public async Task<Realtor> GetByIdAsync(int id)
        {
            return await applicationDbContext.Realtors.Include(x => x.Agency).Include(x => x.ResidenceList).FirstOrDefaultAsync(x => x.Id == id.ToString());
        }

        public async Task UpdateAsync(Realtor realtor)
        {
            var result = await userManager.UpdateAsync(realtor);
            await applicationDbContext.SaveChangesAsync();
            if (!result.Succeeded)
            {
                throw new Exception("Failed to update user");
            }
        }
    }
}
