﻿using System;
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
    public class AgenciesController : ControllerBase
    {
        private readonly FribergAPIContext _context;

        public AgenciesController(FribergAPIContext context)
        {
            _context = context;
        }

        // GET: api/Agencies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agency>>> GetAgency()
        {
            return await _context.Agency.ToListAsync();
        }

        // GET: api/Agencies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Agency>> GetAgency(int id)
        {
            var agency = await _context.Agency.FindAsync(id);

            if (agency == null)
            {
                return NotFound();
            }

            return agency;
        }

        // PUT: api/Agencies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgency(int id, Agency agency)
        {
            if (id != agency.AgencyId)
            {
                return BadRequest();
            }

            _context.Entry(agency).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgencyExists(id))
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

        // POST: api/Agencies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Agency>> PostAgency(Agency agency)
        {
            _context.Agency.Add(agency);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAgency", new { id = agency.AgencyId }, agency);
        }

        // DELETE: api/Agencies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgency(int id)
        {
            var agency = await _context.Agency.FindAsync(id);
            if (agency == null)
            {
                return NotFound();
            }

            _context.Agency.Remove(agency);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AgencyExists(int id)
        {
            return _context.Agency.Any(e => e.AgencyId == id);
        }
    }
}