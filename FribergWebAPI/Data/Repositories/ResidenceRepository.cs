using FribergWebAPI.Data.Interfaces;
using FribergWebAPI.Models;
using Microsoft.EntityFrameworkCore;

//author: Christian Alp, co-author: Johan Kr�ngh
namespace FribergWebAPI.Data.Repositories
{
	public class ResidenceRepository : IResidence
	{
		private readonly ApplicationDbContext applicationDbContext;

		public ResidenceRepository(ApplicationDbContext applicationDbContext)
		{
			this.applicationDbContext = applicationDbContext;
		}

		public async Task<IEnumerable<Residence>> GetAll()
		{
			return await applicationDbContext.Residences.Include(x => x.Municipality).Include(x => x.Category).Include(x => x.Pictures).Include(x => x.Realtor).ThenInclude(x => x.Agency).ToListAsync();
		}

        public async Task<IEnumerable<Residence>> GetAllByAgencyAsync(int agencyId)
        {
            return await applicationDbContext.Residences.Where(x => x.Realtor.Agency.AgencyId == agencyId).Include(x => x.Municipality).Include(x => x.Category).Include(x => x.Pictures).Include(x => x.Realtor).ThenInclude(x => x.Agency).ToListAsync();
        }

        public async Task<IEnumerable<Residence>> GetAllByRealtorAsync(string realtorId)
        {
            return await applicationDbContext.Residences.Where(x => x.Realtor.Id == realtorId).Include(x => x.Municipality).Include(x => x.Category).Include(x => x.Pictures).Include(x => x.Realtor).ThenInclude(x => x.Agency).ToListAsync();
        }

		public async Task<Residence> GetById(int id)
		{
			return await applicationDbContext.Residences.Include(x => x.Municipality).Include(x => x.Category).Include(x => x.Realtor).Include(x=>x.Pictures).FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task Add(Residence residence)
		{
			applicationDbContext.Residences.Add(residence);
			applicationDbContext.Entry(residence.Category).State = EntityState.Unchanged;
			applicationDbContext.Entry(residence.Municipality).State = EntityState.Unchanged;
			applicationDbContext.Entry(residence.Realtor).State = EntityState.Unchanged;
            applicationDbContext.Entry(residence.Realtor.Agency).State = EntityState.Unchanged;
			await applicationDbContext.SaveChangesAsync();
		}

		public async Task Update(Residence residence)
		{
			applicationDbContext.Attach(residence.Category);
			applicationDbContext.Attach(residence.Municipality);
			applicationDbContext.Attach(residence.Realtor);
			applicationDbContext.Entry(residence).State = EntityState.Modified;
			await applicationDbContext.SaveChangesAsync();
		}

		public async Task Delete(int id)
		{
			var residence = await applicationDbContext.Residences.FindAsync(id);
			if (residence != null)
			{
				applicationDbContext.Residences.Remove(residence);
				await applicationDbContext.SaveChangesAsync();	
			}
		}

    }
}
