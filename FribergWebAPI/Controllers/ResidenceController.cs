using AutoMapper;
using FribergWebAPI.Data.Interfaces;
using FribergWebAPI.DTOs;
using FribergWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

//author: Christian Alp
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
			//CrudResidenceDto or the one now?
			var residenceDtos = mapper.Map<List<CRUDResidenceDto>>(residence);
			return Ok(residenceDtos);
		}

		// GET: api/Residence/5
		[HttpGet("{id}")]
		//CrudResidenceDto or the one now?
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

		//This here or up there?	//var residence = mapper.Map<Residence>(residenceDto);
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
