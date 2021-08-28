using CitizensApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitizenLocationTypesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitizenLocationTypesController : ControllerBase
    {
        private readonly CitizensDbContext _context;

        public CitizenLocationTypesController(CitizensDbContext context)
        {
            _context = context;
        }

        // GET: api/CitizenLocationTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitizenLocationType>>> GetCitizenLocationTypes()
        {
            return await _context.CitizenLocationTypes.ToListAsync();
        }

        // GET: api/CitizenLocationTypes/CitizenType/1
        [HttpGet]
        [Route("CitizenType/{citizenTypeId}")]
        public async Task<ActionResult<IEnumerable<CitizenLocationType>>> GetCitizenLocationTypesByCitizenType(int citizenTypeId)
        {
            return await _context.CitizenLocationTypes.Where(clt => clt.CitizenTypeId == citizenTypeId).ToListAsync();
        }

        // GET: api/CitizenLocationTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CitizenLocationType>> GetCitizen(int id)
        {
            var citizenLocationType = await _context.CitizenLocationTypes.FindAsync(id);

            if (citizenLocationType == null)
            {
                return NotFound();
            }

            return citizenLocationType;
        }

        // PUT: api/CitizenLocationTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCitizenLocationType(int id, CitizenLocationType citizenLocationType)
        {
            if (id != citizenLocationType.Id)
            {
                return BadRequest();
            }

            _context.Entry(citizenLocationType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitizenLocationTypeExists(id))
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

        // POST: api/CitizenLocationTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CitizenLocationType>> PostCitizenLocationType(CitizenLocationType citizenLocationType)
        {
            _context.CitizenLocationTypes.Add(citizenLocationType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCitizenLocationType", new { id = citizenLocationType.Id }, citizenLocationType);
        }

        // DELETE: api/CitizenLocationTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCitizenLocationType(int id)
        {
            var citizenLocationType = await _context.CitizenLocationTypes.FindAsync(id);
            if (citizenLocationType == null)
            {
                return NotFound();
            }

            _context.CitizenLocationTypes.Remove(citizenLocationType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CitizenLocationTypeExists(int id)
        {
            return _context.CitizenLocationTypes.Any(e => e.Id == id);
        }
    }
}
