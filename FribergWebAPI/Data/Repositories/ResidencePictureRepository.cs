using FribergWebAPI.Data.Interfaces;
using FribergWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FribergWebAPI.Data.Repositories
{
    public class ResidencePictureRepository : IResidencePicture
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ResidencePictureRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task AddAsync(ResidencePicture residencePicture)
        {
            await applicationDbContext.ResidencePicture.AddAsync(residencePicture);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(ResidencePicture residencePicture)
        {
            applicationDbContext.ResidencePicture.Remove(residencePicture);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ResidencePicture>> GetAllAsync()
        {
            return await applicationDbContext.ResidencePicture.ToListAsync();
        }

        public async Task<ResidencePicture> GetByIdAsync(int id)
        {
            return await applicationDbContext.ResidencePicture.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(ResidencePicture residencePicture)
        {
            applicationDbContext.Update(residencePicture);
            await applicationDbContext.SaveChangesAsync();
        }
    }
}
