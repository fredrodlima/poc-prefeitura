using CitizensApi.Messaging.Producers;
using CitizensApi.Models;
using CitizensApi.Models.TaxCalculation;
using CitizensApi.Services;
using CitizensApi.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitizensApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitizensController : ControllerBase
    {
        private readonly CitizensDbContext _context;
        private readonly MessageProducer _messageProducer;
        private readonly IConfiguration _configuration;

        public CitizensController(IConfiguration configuration, CitizensDbContext context, MessageProducer messageProducer)
        {
            _context = context;
            _messageProducer = messageProducer;
            _configuration = configuration;
        }

        // GET: api/Citizens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Citizen>>> GetCitizens()
        {
            return await _context.Citizens.ToListAsync();
        }

        // GET: api/Citizens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Citizen>> GetCitizen(int id)
        {
            var citizen = await _context.Citizens.FindAsync(id);

            if (citizen == null)
            {
                return NotFound();
            }

            return citizen;
        }
        
        [HttpGet("{id}/localities")]
        public async Task<ActionResult<IEnumerable<CitizenLocalitiesModel>>> GetCitizenLocalities(int id)
        {
            var citizen = await _context.Citizens.FindAsync(id);

            if (citizen == null)
            {
                return NotFound();
            }
            var citizenLocalitiesService = new CitizenLocalitiesService(_configuration);
            return citizenLocalitiesService.GetMyLocalities(citizen.Id).ToList();
        }

        [HttpPost("{id}/requestTaxCalculation")]
        public async Task<IActionResult> RequestTaxCalculation(int id)
        {
            var citizen = await _context.Citizens.FindAsync(id);

            if (citizen == null)
            {
                return NotFound();
            }

            var @event = new RequestTaxCalculationCreated
            {
                Id = Guid.NewGuid(),
                CitizenId = citizen.Id,
                Timestamp = DateTime.UtcNow
            };

            await _messageProducer.PublishAsync(@event);

            return Ok() ;
        }

        // PUT: api/Citizens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCitizen(int id, Citizen citizen)
        {
            if (id != citizen.Id)
            {
                return BadRequest();
            }

            _context.Entry(citizen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitizenExists(id))
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

        // POST: api/Citizens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Citizen>> PostCitizen(Citizen citizen)
        {
            _context.Citizens.Add(citizen);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCitizen", new { id = citizen.Id }, citizen);
        }

        // DELETE: api/Citizens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCitizen(int id)
        {
            var citizen = await _context.Citizens.FindAsync(id);
            if (citizen == null)
            {
                return NotFound();
            }

            _context.Citizens.Remove(citizen);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CitizenExists(int id)
        {
            return _context.Citizens.Any(e => e.Id == id);
        }
    }
}
