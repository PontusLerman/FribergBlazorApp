using AutoMapper;
using FribergWebAPI.Data.Interfaces;
using FribergWebAPI.DTOs;
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
		private readonly IMapper _mapper;

		public CategoryController(ICategory categoryRepository, IMapper mapper)
		{
			this.categoryRepository = categoryRepository;
			_mapper = mapper;
		}

		// GET: api/Category
		[HttpGet]
		public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
		{
			var categories = await categoryRepository.GetAll();
			var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
			return Ok(categoryDtos);
		}

		// GET: api/Category/5
		[HttpGet("{id}")]
		public async Task<ActionResult<CategoryDto>> GetCategory(int id)
		{
			var categories = await categoryRepository.GetById(id);
			var categoryDtos = _mapper.Map<CategoryDto>(categories);
			if (categories == null)
			{
				return NotFound();
			}

			return Ok(categoryDtos);
		}

		// POST: api/Category
		[HttpPost]
		public async Task<ActionResult<Category>> PostCategory(CategoryDto categoryDto)
		{
			var category = _mapper.Map<Category>(categoryDto);
			await categoryRepository.Add(category);
			return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
		}

		// PUT: api/Category/5
		[HttpPut("{id}")]
		public async Task<ActionResult<Category>> PutCategory(int id, CategoryDto categoryDto)
		{
			var category = _mapper.Map<Category>(categoryDto);
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