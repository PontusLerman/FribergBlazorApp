using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FribergWebAPI.Data;
using FribergWebAPI.Models;

namespace FribergWebAPI.Controllers
{
    //Pontus
    [Route("api/[controller]")]
    [ApiController]
    public class RealtorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RealtorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Realtors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Realtor>>> Getrealtors()
        {
            return await _context.realtors.ToListAsync();
        }

        // GET: api/Realtors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Realtor>> GetRealtor(int id)
        {
            var realtor = await _context.realtors.FindAsync(id);

            if (realtor == null)
            {
                return NotFound();
            }

            return realtor;
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

            _context.Entry(realtor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RealtorExists(id))
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
            _context.realtors.Add(realtor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRealtor", new { id = realtor.Id }, realtor);
        }

        // DELETE: api/Realtors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRealtor(int id)
        {
            var realtor = await _context.realtors.FindAsync(id);
            if (realtor == null)
            {
                return NotFound();
            }

            _context.realtors.Remove(realtor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RealtorExists(int id)
        {
            return _context.realtors.Any(e => e.Id == id);
        }
    }
}
