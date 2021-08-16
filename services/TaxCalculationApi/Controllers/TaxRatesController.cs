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
    public class TaxRatesController : ControllerBase
    {
        private readonly TaxesDbContext _context;

        public TaxRatesController(TaxesDbContext context)
        {
            _context = context;
        }

        // GET: api/TaxRates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaxRate>>> GetTaxRates()
        {
            return await _context.TaxRates.ToListAsync();
        }

        // GET: api/TaxRates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaxRate>> GetTaxRate(int id)
        {
            var taxRate = await _context.TaxRates.FindAsync(id);

            if (taxRate == null)
            {
                return NotFound();
            }

            return taxRate;
        }

        // PUT: api/TaxRates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaxRate(int id, TaxRate taxRate)
        {
            if (id != taxRate.Id)
            {
                return BadRequest();
            }

            _context.Entry(taxRate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaxRateExists(id))
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

        // POST: api/TaxRates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaxRate>> PostTaxRate(TaxRate taxRate)
        {
            _context.TaxRates.Add(taxRate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaxRate", new { id = taxRate.Id }, taxRate);
        }

        // DELETE: api/TaxRates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaxRate(int id)
        {
            var taxRate = await _context.TaxRates.FindAsync(id);
            if (taxRate == null)
            {
                return NotFound();
            }

            _context.TaxRates.Remove(taxRate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaxRateExists(int id)
        {
            return _context.TaxRates.Any(e => e.Id == id);
        }
    }
}
