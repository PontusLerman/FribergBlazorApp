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

namespace FribergWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AgenciesController : ControllerBase //author: Johan Krångh
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
		public async Task<ActionResult<IEnumerable<CRUDAgencyDto>>> GetAgency()
		{
			var agencies = await agencyRepo.GetAllAsync();
			var CRUDAgencyDto = mapper.Map<List<CRUDAgencyDto>>(agencies);
			return Ok(CRUDAgencyDto);
		}

		// GET: api/Agencies/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Agency>> GetAgency(int id)
		{
			var agency = await agencyRepo.GetByIdAsync(id);

			if (agency == null)
			{
				return NotFound();
			}

			return agency;
		}

		// PUT: api/Agencies/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<ActionResult<Agency>> PutAgency(int id, CRUDAgencyDto agencyDto)
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
		public async Task<ActionResult<Agency>> PostAgency(CRUDAgencyDto agencyDto)
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
