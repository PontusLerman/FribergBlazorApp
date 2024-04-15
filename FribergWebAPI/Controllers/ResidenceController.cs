using FribergWebAPI.Data;
using FribergWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

//author: Christian
namespace FribergWebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ResidenceController : ControllerBase
	{
		private readonly IResidence residenceRepository;

		public ResidenceController(IResidence residenceRepository)
		{
			this.residenceRepository = residenceRepository;
		}

		// GET: api/Residence
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Residence>>> GetResidence()
		{
			var residence = await residenceRepository.GetAll();
			return Ok(residence);
		}

		// GET: api/Residence/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Residence>> GetResidence(int id)
		{
			var residence = await residenceRepository.GetById(id);
			
			if (residence == null)
			{
				return NotFound();
			}

			return Ok(residence);
		}

		// POST: api/Residence
		[HttpPost]
		public async Task<ActionResult<Residence>> PostResidence(Residence residence)
		{
			await residenceRepository.Add(residence);
			return CreatedAtAction(nameof(GetResidence), new { id = residence.Id }, residence);
		}

		// PUT: api/Residence/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutResidence(int id, Residence residence)
		{
			if (id != residence.Id)
			{
				return BadRequest();
			}

			await residenceRepository.Update(residence);
			return NoContent();
		}

		// DELETE: api/Residence/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteResidence(int id)
		{
			await residenceRepository.Delete(id);
			return NoContent();
		}
	}
}
