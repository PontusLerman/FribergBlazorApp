using FribergWebAPI.Data.Interfaces;
using FribergWebAPI.Models;
using Microsoft.EntityFrameworkCore;

//author: Christian
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

		public async Task<Residence> GetById(int id)
		{
			return await applicationDbContext.Residences.FindAsync(id);
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
