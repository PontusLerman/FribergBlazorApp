using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FribergWebAPI.Data;
using FribergWebAPI.Models;
using FribergWebAPI.Data.Interfaces;

namespace FribergWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ResidencePicturesController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly IResidencePicture repo;

		public ResidencePicturesController(ApplicationDbContext context, IResidencePicture repo)
		{
			_context = context;
			this.repo = repo;
		}

		// GET: api/ResidencePictures
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ResidencePicture>>> GetResidencePicture()
		{
			var residencePictures = await repo.GetAllAsync();
			return Ok(residencePictures);
		}

		// GET: api/ResidencePictures/5
		[HttpGet("{id}")]
		public async Task<ActionResult<ResidencePicture>> GetResidencePicture(int id)
		{
			var residencePicture = await repo.GetByIdAsync(id);

			if (residencePicture == null)
			{
				return NotFound();
			}

			return residencePicture;
		}

		// PUT: api/ResidencePictures/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutResidencePicture(int id, ResidencePicture residencePicture)
		{
			if (id != residencePicture.Id)
			{
				return BadRequest();
			}

			await repo.UpdateAsync(residencePicture);

			return NoContent();
		}

		// POST: api/ResidencePictures
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<ResidencePicture>> PostResidencePicture(ResidencePicture residencePicture)
		{
			await repo.AddAsync(residencePicture);
			return CreatedAtAction(nameof(GetResidencePicture), new { id = residencePicture.Id }, residencePicture);	
		}

		// DELETE: api/ResidencePictures/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteResidencePicture(int id)
		{
			var residencePicture = await repo.GetByIdAsync(id);
			if (residencePicture == null)
			{
				return NotFound();
			}

			await repo.DeleteAsync(residencePicture);

			return NoContent();
		}
	}
}
