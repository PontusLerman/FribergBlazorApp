using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FribergWebAPI.Models;
using FribergWebAPI.Data.Interfaces;
using FribergWebAPI.DTOs;
using AutoMapper;

//author: Christian Alp, Johan Krångh
namespace FribergWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AgenciesController : ControllerBase 
	{
		private readonly IAgency agencyRepo;
		private readonly IMapper mapper;

		public AgenciesController(IAgency agencyRepo, IMapper mapper)
		{
			this.agencyRepo = agencyRepo;
			this.mapper = mapper;
		}

		// GET: api/Agencies
		[HttpGet]
		public async Task<ActionResult<IEnumerable<AgencyDto>>> GetAgency()
		{
			var agency = await agencyRepo.GetAllAsync();
			var agencyDtos = mapper.Map<List<AgencyDto>>(agency);
			return Ok(agencyDtos);
		}

		// GET: api/Agencies/5
		[HttpGet("{id}")]
		public async Task<ActionResult<AgencyDto>> GetAgency(int id)
		{
			var agency = await agencyRepo.GetByIdAsync(id);
			var agencyDtos = mapper.Map<AgencyDto>(agency);

			if (agency == null)
			{
				return NotFound();
			}

			return Ok(agencyDtos);
		}

		// PUT: api/Agencies/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<ActionResult<AgencyDto>> PutAgency(int id, AgencyDto agencyDto)
		{
			var agency = mapper.Map<Agency>(agencyDto);
			if (id != agency.AgencyId)
			{
				return BadRequest();
			}

			await agencyRepo.UpdateAsync(agency);

			return NoContent();
		}

		// POST: api/Agencies
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<AgencyDto>> PostAgency(AgencyDto agencyDto)
		{
			var agency = mapper.Map<Agency>(agencyDto);
			await agencyRepo.AddAsync(agency);

			return CreatedAtAction("GetAgency", new { id = agency.AgencyId }, agency);
		}

		// DELETE: api/Agencies/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAgency(int id)
		{
			var agency = await agencyRepo.GetByIdAsync(id);
			if (agency == null)
			{
				return NotFound();
			}

			await agencyRepo.DeleteAsync(agency);

			return NoContent();
		}
	}
}
