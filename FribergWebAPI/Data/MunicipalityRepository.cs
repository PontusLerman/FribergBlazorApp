﻿using FribergWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FribergWebAPI.Data
{
    public class MunicipalityRepository : IMunicipality //author: Johan
    {
        private readonly ApplicationDbContext applicationDbContext;

        public MunicipalityRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task AddAsync(Municipality municipality)
        {
            await applicationDbContext.Municipality.AddAsync(municipality);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Municipality municipality)
        {
            applicationDbContext.Municipality.Remove(municipality);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Municipality>> GetAllAsync()
        {
            return await applicationDbContext.Municipality.ToListAsync();
        }

        public async Task<Municipality> GetByIdAsync(int id)
        {
            return await applicationDbContext.Municipality.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Municipality municipality)
        {
            applicationDbContext.Update(municipality);
            await applicationDbContext.SaveChangesAsync();
        }
    }
}
