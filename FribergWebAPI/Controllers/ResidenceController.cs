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
	public class ResidenceController : ControllerBase
	{
		private readonly IResidence residenceRepository;
        private readonly IMapper mapper;

        public ResidenceController(IResidence residenceRepository, IMapper mapper)
		{
			this.residenceRepository = residenceRepository;
            this.mapper = mapper;
        }

		// GET: api/Residence
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ResidenceDto>>> GetResidence()
		{
			var residence = await residenceRepository.GetAll();
			var residenceDtos = mapper.Map<List<ResidenceDto>>(residence);
            return Ok(residenceDtos);
		}

		// GET: api/Residence/5
		[HttpGet("{id}")]
		public async Task<ActionResult<ResidenceDto>> GetResidence(int id)
		{
			var residence = await residenceRepository.GetById(id);
            var residenceDtos = mapper.Map<List<ResidenceDto>>(residence);

            if (residenceDtos == null)
			{
				return NotFound();
			}

			return Ok(residenceDtos);
		}

		// POST: api/Residence
		[HttpPost]
		public async Task<ActionResult<Residence>> PostResidence(CRUDResidenceDto residenceDto)
		{
			var residence = mapper.Map<Residence>(residenceDto);
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
