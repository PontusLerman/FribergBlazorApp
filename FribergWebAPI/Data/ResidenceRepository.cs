using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FribergWebAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace FribergWebAPI.Data
{
	public class ResidenceRepository : IResidence
	{
		 private readonly ApplicationDbContext context;

		public ResidenceRepository(ApplicationDbContext context)
		{
			this.context = context;
		}
		
		public async Task<IEnumerable<Residence>> GetAll()
		{
			return await context.Residences.ToListAsync();
		}

		public async Task<Residence> GetById(int id)
		{
			return await context.Residences.FindAsync(id);
		}
		
		public async Task Add(Residence residence)
		{
			context.Residences.Add(residence);
			await context.SaveChangesAsync();
		}
		
		public async Task Update(Residence residence)
		{
			context.Entry(residence).State = EntityState.Modified;
			await context.SaveChangesAsync();		
		}

		public async Task Delete(int id)
		{
			var residence = await context.Residences.FindAsync(id);
			if (residence != null)
			{
				context.Residences.Remove(residence);
				await context.SaveChangesAsync();
			}
		}
	}
}