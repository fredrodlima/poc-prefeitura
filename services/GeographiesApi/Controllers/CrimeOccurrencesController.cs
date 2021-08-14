using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeographiesApi.Models;

namespace GeographiesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrimeOccurrencesController : ControllerBase
    {
        private readonly GeographiesContext _context;

        public CrimeOccurrencesController(GeographiesContext context)
        {
            _context = context;
        }

        // GET: api/CrimeOccurrences
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CrimeOccurrence>>> GetCrimeOccurrences()
        {
            return await _context.CrimeOccurrences.ToListAsync();
        }

        // GET: api/CrimeOccurrences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CrimeOccurrence>> GetCrimeOccurrence(int id)
        {
            var crimeOccurrence = await _context.CrimeOccurrences.FindAsync(id);

            if (crimeOccurrence == null)
            {
                return NotFound();
            }

            return crimeOccurrence;
        }

        // PUT: api/CrimeOccurrences/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCrimeOccurrence(int id, CrimeOccurrence crimeOccurrence)
        {
            if (id != crimeOccurrence.Id)
            {
                return BadRequest();
            }

            _context.Entry(crimeOccurrence).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CrimeOccurrenceExists(id))
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

        // POST: api/CrimeOccurrences
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CrimeOccurrence>> PostCrimeOccurrence(CrimeOccurrence crimeOccurrence)
        {
            _context.CrimeOccurrences.Add(crimeOccurrence);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCrimeOccurrence", new { id = crimeOccurrence.Id }, crimeOccurrence);
        }

        // DELETE: api/CrimeOccurrences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCrimeOccurrence(int id)
        {
            var crimeOccurrence = await _context.CrimeOccurrences.FindAsync(id);
            if (crimeOccurrence == null)
            {
                return NotFound();
            }

            _context.CrimeOccurrences.Remove(crimeOccurrence);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CrimeOccurrenceExists(int id)
        {
            return _context.CrimeOccurrences.Any(e => e.Id == id);
        }
    }
}
