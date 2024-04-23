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
			var Categories = await categoryRepository.GetAll();
			return Ok(Categories);
		}

		// GET: api/Category/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Category>> GetCategory(int id)
		{
			var Category = await categoryRepository.GetById(id);
			if (Category == null)
			{
				return NotFound();
			}

			return Ok(Category);
		}

		// POST: api/Category
		[HttpPost]
		public async Task<ActionResult<Category>> PostHousingCategory(Category category)
		{
            await categoryRepository.Add(category);
			return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
		}

		// PUT: api/Category/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutCategory(int id, Category category)
		{
			if (id != category.Id)
			{
				return BadRequest();
			}

			await categoryRepository.Update(category);
			return NoContent();
		}

		// DELETE: api/Category/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			await categoryRepository.Delete(id);
			return NoContent();
		}
	}
}