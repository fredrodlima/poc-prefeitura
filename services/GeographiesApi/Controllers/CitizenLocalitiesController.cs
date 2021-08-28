using GeographiesApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeographiesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitizenLocalitiesController : ControllerBase
    {
        private readonly GeographiesContext _context;

        public CitizenLocalitiesController(GeographiesContext context)
        {
            _context = context;
        }

        // GET: api/CitizenLocalities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitizenLocality>>> GetCitizenLocalities()
        {
            return await _context.CitizenLocalities.ToListAsync();
        }

        // GET: api/CitizenLocalities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CitizenLocality>> GetCitizenLocality(int id)
        {
            var citizenLocality = await _context.CitizenLocalities.FindAsync(id);

            if (citizenLocality == null)
            {
                return NotFound();
            }

            return citizenLocality;
        }

        // GET: api/CitizenLocalities/5
        [HttpGet("localities/{citizenId}")]
        public async Task<ActionResult<IEnumerable<CitizenLocality>>> GetCitizenLocalities(int citizenId)
        {
            var citizenLocality = await _context.CitizenLocalities.Where(cl => cl.CitizenId == citizenId).ToListAsync();

            if (citizenLocality == null)
            {
                return NotFound();
            }

            return citizenLocality;
        }

        // PUT: api/CitizenLocalities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCitizenLocality(int id, CitizenLocality citizenLocality)
        {
            if (id != citizenLocality.Id)
            {
                return BadRequest();
            }

            _context.Entry(citizenLocality).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitizenLocalityExists(id))
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

        // POST: api/CitizenLocalities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CitizenLocality>> PostCitizenLocality(CitizenLocality citizenLocality)
        {
            _context.CitizenLocalities.Add(citizenLocality);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCitizenLocality", new { id = citizenLocality.Id }, citizenLocality);
        }

        // DELETE: api/CitizenLocalities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCitizenLocality(int id)
        {
            var citizenLocality = await _context.CitizenLocalities.FindAsync(id);
            if (citizenLocality == null)
            {
                return NotFound();
            }

            _context.CitizenLocalities.Remove(citizenLocality);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CitizenLocalityExists(int id)
        {
            return _context.CitizenLocalities.Any(e => e.Id == id);
        }
    }
}
