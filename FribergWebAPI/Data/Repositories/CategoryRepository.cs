using FribergWebAPI.Data.Interfaces;
using FribergWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FribergWebAPI.Data.Repositories
{
    public class CategoryRepository : ICategory
    {
        private readonly ApplicationDbContext applicationDbContext;

        public CategoryRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await applicationDbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await applicationDbContext.Categories.FindAsync(id);
        }

        public async Task AddAsync(Category category)
        {
            await applicationDbContext.Categories.AddAsync(category);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            applicationDbContext.Entry(category).State = EntityState.Modified;
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            applicationDbContext.Categories.Remove(category);
            await applicationDbContext.SaveChangesAsync();
        }
    }
}