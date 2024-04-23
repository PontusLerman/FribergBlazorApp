using FribergWebAPI.Data.Interfaces;
using FribergWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

//author: Christian
namespace FribergWebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CategoryController : ControllerBase
	{
		private readonly ICategory categoryRepository;

		public CategoryController(ICategory categoryRepository)
		{
			this.categoryRepository = categoryRepository;
		}

		// GET: api/Category
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
		{
			var Categories = await categoryRepository.GetAllAsync();
			return Ok(Categories);
		}

		// GET: api/Category/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Category>> GetCategory(int id)
		{
			var Category = await categoryRepository.GetByIdAsync(id);
			if (Category == null)
			{
				return NotFound();
			}

			return Ok(Category);
		}

		// POST: api/Category
		[HttpPost]
		public async Task<ActionResult<Category>> PostHousingCategory(Category Category)
		{
			await categoryRepository.AddAsync(Category);
			return CreatedAtAction(nameof(GetCategory), new { id = Category.Id }, Category);
		}

		// PUT: api/Category/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutCategory(int id, Category Category)
		{
			if (id != Category.Id)
			{
				return BadRequest();
			}

			await categoryRepository.UpdateAsync(Category);
			return NoContent();
		}

		// DELETE: api/Category/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategory(Category category)
		{
			await categoryRepository.DeleteAsync(category);
			return NoContent();
		}
	}
}