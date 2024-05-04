using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FribergWebAPI.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using FribergWebAPI.Data.Interfaces;
using AutoMapper;
using FribergWebAPI.DTOs;

namespace FribergWebAPI.Controllers
{
    //Pontus
    [Route("api/[controller]")]
    [ApiController]
    public class RealtorsController : ControllerBase
    {

        private readonly IRealtor realtorRepository;
        private readonly IMapper mapper;

        public RealtorsController(IRealtor realtorRepository, IMapper mapper)
        {            
            this.realtorRepository = realtorRepository;
            this.mapper = mapper;
        }

        // GET: api/Realtors
        [HttpGet]
        //[ProducesResponseType(typeof(RealtorDto), 200)]
        public async Task<ActionResult<IEnumerable<RealtorDto>>> Getrealtors()
        {
            var realtors = await realtorRepository.GetAllAsync();
            var realtorDtos = mapper.Map<List<RealtorDto>>(realtors);
            return Ok(realtorDtos);
        }

        // GET: api/Realtors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RealtorDto>> GetRealtor(int id)
        {
            var realtor = await realtorRepository.GetByIdAsync(id);
            var realtorDto = mapper.Map<RealtorDto>(realtor);
            if (realtorDto == null)
            {
                return NotFound();
            }

            return realtorDto;
        }

        // PUT: api/Realtors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRealtor(int id, Realtor realtor)
        {
            if (id != realtor.Id)
            {
                return BadRequest();
            }
        
            //_context.Entry(realtor).State = EntityState.Modified;

            try
            {
                await realtorRepository.UpdateAsync(realtor);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RealtorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Realtors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Realtor>> PostRealtor(Realtor realtor)
        {
            await realtorRepository.AddAsync(realtor);
            return CreatedAtAction("GetRealtor", new { id = realtor.Id }, realtor);
        }

        // DELETE: api/Realtors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRealtor(int id)
        {
            var realtor = await realtorRepository.GetByIdAsync(id); //_context.realtors.FindAsync(id);
            if (realtor == null)
            {
                return NotFound();
            }

            await realtorRepository.DeleteAsync(realtor);

            return NoContent();
        }

        private async Task<bool> RealtorExists(int id)
        {
            if(await realtorRepository.GetByIdAsync(id) != null) 
            {  
                return true; 
            }
            return false;
        }
    }
}
