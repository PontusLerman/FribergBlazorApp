using FribergWebAPI.Data.Interfaces;
using FribergWebAPI.Models;
using Microsoft.EntityFrameworkCore;

//author: Johan Krångh, Christian Alp
namespace FribergWebAPI.Data.Repositories
{
	public class AgencyRepository : IAgency
	{
		private readonly ApplicationDbContext applicationDbContext;

		public AgencyRepository(ApplicationDbContext applicationDbContext)
		{
			this.applicationDbContext = applicationDbContext;
		}

		public async Task AddAsync(Agency agency)
		{
			await applicationDbContext.Agency.AddAsync(agency);
			await applicationDbContext.SaveChangesAsync();
		}

		public async Task DeleteAsync(Agency agency)
		{
			applicationDbContext.Agency.Remove(agency);
			await applicationDbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<Agency>> GetAllAsync()
		{
			return await applicationDbContext.Agency.Include(a => a.Employees).ToListAsync();
		}

		public async Task<Agency> GetByIdAsync(int id)
		{
			return await applicationDbContext.Agency.Include(a => a.Employees).FirstOrDefaultAsync(x => x.AgencyId == id);
		}

		public async Task UpdateAsync(Agency agency)
		{
			applicationDbContext.Update(agency);
			await applicationDbContext.SaveChangesAsync();
		}
	}
}
