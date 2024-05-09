using FribergWebAPI.Data.Interfaces;
using FribergWebAPI.Models;
using Microsoft.EntityFrameworkCore;

//author: Christian Alp
namespace FribergWebAPI.Data.Repositories
{
	public class CategoryRepository : ICategory
	{
		private readonly ApplicationDbContext applicationDbContext;

		public CategoryRepository(ApplicationDbContext applicationDbContext)
		{
			this.applicationDbContext = applicationDbContext;
		}

		public async Task<IEnumerable<Category>> GetAll()
		{
			return await applicationDbContext.Categories.ToListAsync();
		}

		public async Task<Category> GetById(int id)
		{
			return await applicationDbContext.Categories.FindAsync(id);
		}

		public async Task Add(Category category)
		{
			await applicationDbContext.Categories.AddAsync(category);
			await applicationDbContext.SaveChangesAsync();
		}

		public async Task Update(Category category)
		{
			applicationDbContext.Entry(category).State = EntityState.Modified;
			await applicationDbContext.SaveChangesAsync();
		}

		public async Task Delete(int id)
		{
			var category = await applicationDbContext.Categories.FindAsync(id);
			if (category != null)
			{
				applicationDbContext.Categories.Remove(category);
				await applicationDbContext.SaveChangesAsync();
			}
		}
	}
}