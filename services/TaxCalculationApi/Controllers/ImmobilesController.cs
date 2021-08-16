using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxCalculationApi.Models;

namespace TaxCalculationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImmobilesController : ControllerBase
    {
        private readonly TaxesDbContext _context;

        public ImmobilesController(TaxesDbContext context)
        {
            _context = context;
        }

        // GET: api/Immobiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Immobile>>> GetImmobiles()
        {
            return await _context.Immobiles.ToListAsync();
        }

        // GET: api/Immobiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Immobile>> GetImmobile(int id)
        {
            var immobile = await _context.Immobiles.FindAsync(id);

            if (immobile == null)
            {
                return NotFound();
            }

            return immobile;
        }

        // PUT: api/Immobiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImmobile(int id, Immobile immobile)
        {
            if (id != immobile.Id)
            {
                return BadRequest();
            }

            _context.Entry(immobile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImmobileExists(id))
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

        // POST: api/Immobiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Immobile>> PostImmobile(Immobile immobile)
        {
            _context.Immobiles.Add(immobile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImmobile", new { id = immobile.Id }, immobile);
        }

        // DELETE: api/Immobiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImmobile(int id)
        {
            var immobile = await _context.Immobiles.FindAsync(id);
            if (immobile == null)
            {
                return NotFound();
            }

            _context.Immobiles.Remove(immobile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImmobileExists(int id)
        {
            return _context.Immobiles.Any(e => e.Id == id);
        }
    }
}
