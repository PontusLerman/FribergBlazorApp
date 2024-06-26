using AutoMapper;
using FribergWebAPI.Data.Interfaces;
using FribergWebAPI.DTOs;
using FribergWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

//author: Christian Alp, co-author: Johan Kr�ngh
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
		public async Task<ActionResult<IEnumerable<CRUDResidenceDto>>> GetResidence()
		{
			var residence = await residenceRepository.GetAll();
			var residenceDtos = mapper.Map<List<CRUDResidenceDto>>(residence);
			return Ok(residenceDtos);
		}
        
		[HttpGet]
        [Route("residences-by-realtor/{realtorId}")]
        public async Task<ActionResult<IEnumerable<CRUDResidenceDto>>> GetResidencesByRealtor(string realtorId)
        {
            var residence = await residenceRepository.GetAllByRealtorAsync(realtorId);
            var residenceDtos = mapper.Map<List<CRUDResidenceDto>>(residence);
            return Ok(residenceDtos);
        }

        [HttpGet]
		[Route("residences-by-agency/{agencyId}")]
        public async Task<ActionResult<IEnumerable<CRUDResidenceDto>>> GetResidencesByAgency(int agencyId)
        {
            var residence = await residenceRepository.GetAllByAgencyAsync(agencyId);           
            var residenceDtos = mapper.Map<List<CRUDResidenceDto>>(residence);
            return Ok(residenceDtos);
        }

        // GET: api/Residence/5
        [HttpGet("{id}")]
		public async Task<ActionResult<CRUDResidenceDto>> GetResidence(int id)
		{
			var residence = await residenceRepository.GetById(id);
			var residenceDtos = mapper.Map<CRUDResidenceDto>(residence);

			if (residence == null)
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
		public async Task<IActionResult> PutResidence(int id, CRUDResidenceDto residenceDto)
		{
			var residence = mapper.Map<Residence>(residenceDto);
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
