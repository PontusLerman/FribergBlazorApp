using FribergWebAPI.Data.Interfaces;
using FribergWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FribergWebAPI.Data.Repositories
{
	public class RealtorRepository : IRealtor
	{
		private readonly ApplicationDbContext dbContext;

		public RealtorRepository(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task AddAsync(Realtor realtor)
		{
			dbContext.Realtors.Add(realtor);
			dbContext.Entry(realtor.Agency).State = EntityState.Unchanged;
			await dbContext.SaveChangesAsync();
		}

		public async Task DeleteAsync(Realtor realtor)
		{
			dbContext.Realtors.Remove(realtor);
			await dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<Realtor>> GetAllAsync()
		{
            return await dbContext.Realtors.Include(x => x.Agency).Include(x => x.ResidenceList).OrderBy(r => r.FirstName).ToListAsync();

		}

		public async Task<Realtor> GetByIdAsync(int id)
		{
            return await dbContext.Realtors.Include(x => x.Agency).Include(x => x.ResidenceList).FirstOrDefaultAsync(x => x.Id == id.ToString());
		}

		public async Task UpdateAsync(Realtor realtor)
		{
			dbContext.Update(realtor);
			dbContext.Attach(realtor.Agency);
			await dbContext.SaveChangesAsync();
		}
	}
}
