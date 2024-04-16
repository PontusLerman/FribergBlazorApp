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
    [Route("api/[controller]")]
    [ApiController]
    public class ResidencePicturesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ResidencePicturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ResidencePictures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResidencePicture>>> GetResidencePicture()
        {
            return await _context.ResidencePicture.Include(r=>r.Residence).ToListAsync();
        }

        // GET: api/ResidencePictures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResidencePicture>> GetResidencePicture(int id)
        {
            var residencePicture = await _context.ResidencePicture.FindAsync(id);

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

            _context.Entry(residencePicture.Residence).State = EntityState.Unchanged;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResidencePictureExists(id))
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

        // POST: api/ResidencePictures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ResidencePicture>> PostResidencePicture(ResidencePicture residencePicture)
        {
            _context.ResidencePicture.Add(residencePicture);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResidencePicture", new { id = residencePicture.Id }, residencePicture);
        }

        // DELETE: api/ResidencePictures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResidencePicture(int id)
        {
            var residencePicture = await _context.ResidencePicture.FindAsync(id);
            if (residencePicture == null)
            {
                return NotFound();
            }

            _context.ResidencePicture.Remove(residencePicture);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ResidencePictureExists(int id)
        {
            return _context.ResidencePicture.Any(e => e.Id == id);
        }
    }
}
