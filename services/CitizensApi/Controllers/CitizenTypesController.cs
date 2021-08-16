using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CitizensApi.Models;

namespace CitizensApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitizenTypesController : ControllerBase
    {
        private readonly CitizensDbContext _context;

        public CitizenTypesController(CitizensDbContext context)
        {
            _context = context;
        }

        // GET: api/CitizenTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitizenType>>> GetCitizenTypes()
        {
            return await _context.CitizenTypes.ToListAsync();
        }

        // GET: api/CitizenTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CitizenType>> GetCitizenType(int id)
        {
            var citizenType = await _context.CitizenTypes.FindAsync(id);

            if (citizenType == null)
            {
                return NotFound();
            }

            return citizenType;
        }

        // PUT: api/CitizenTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCitizenType(int id, CitizenType citizenType)
        {
            if (id != citizenType.Id)
            {
                return BadRequest();
            }

            _context.Entry(citizenType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitizenTypeExists(id))
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

        // POST: api/CitizenTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CitizenType>> PostCitizenType(CitizenType citizenType)
        {
            _context.CitizenTypes.Add(citizenType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCitizenType", new { id = citizenType.Id }, citizenType);
        }

        // DELETE: api/CitizenTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCitizenType(int id)
        {
            var citizenType = await _context.CitizenTypes.FindAsync(id);
            if (citizenType == null)
            {
                return NotFound();
            }

            _context.CitizenTypes.Remove(citizenType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CitizenTypeExists(int id)
        {
            return _context.CitizenTypes.Any(e => e.Id == id);
        }
    }
}
