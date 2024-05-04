using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FribergWebAPI.Models;
using FribergWebAPI.Data.Interfaces;
using AutoMapper;
using FribergWebAPI.DTOs;

namespace FribergWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipalitiesController : ControllerBase //author: Johan
    {
        private readonly IMunicipality municipalityRepo;
        private readonly IMapper mapper;

        public MunicipalitiesController(IMunicipality municipalityRepo, IMapper mapper)
        {
            this.municipalityRepo = municipalityRepo;
            this.mapper = mapper;
        }

        // GET: api/Municipalities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MunicipalityDto>>> GetMunicipality()
        {
            var municipalities = await municipalityRepo.GetAllAsync();
            var municipalityDtos = mapper.Map<List<MunicipalityDto>>(municipalities);
            return Ok(municipalityDtos);
        }

        // GET: api/Municipalities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MunicipalityDto>> GetMunicipality(int id)
        {
            var municipality = await municipalityRepo.GetByIdAsync(id);
            var municipalityDto = mapper.Map<MunicipalityDto>(municipality);
            if (municipality == null)
            {
                return NotFound();
            }

            return municipalityDto;
        }

        // PUT: api/Municipalities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMunicipality(int id, MunicipalityDto municipalityDto)
        {
            if (id != municipalityDto.Id)
            {
                return BadRequest();
            }

            var municipality = mapper.Map<Municipality>(municipalityDto);
            await municipalityRepo.UpdateAsync(municipality);

            return NoContent();
        }

        // POST: api/Municipalities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Municipality>> PostMunicipality(MunicipalityDto municipalityDto)
        {
            var municipality = mapper.Map<Municipality>(municipalityDto);
            await municipalityRepo.AddAsync(municipality);

            return CreatedAtAction("GetMunicipality", new { id = municipality.Id }, municipality);
        }

        // DELETE: api/Municipalities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMunicipality(int id)
        {
            var municipality = await municipalityRepo.GetByIdAsync(id);
            if (municipality == null)
            {
                return NotFound();
            }

            await municipalityRepo.DeleteAsync(municipality);

            return NoContent();
        }
    }
}
