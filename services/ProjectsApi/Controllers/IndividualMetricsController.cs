using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectsApi.Messaging.Producers;
using ProjectsApi.Models;

namespace IndividualMetricsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndividualMetricsController : ControllerBase
    {
        private readonly ProjectsDbContext _context;
        private readonly MessageProducer _messageProducer;

        public IndividualMetricsController(ProjectsDbContext context, MessageProducer messageProducer)
        {
            _context = context;
            _messageProducer = messageProducer;
        }

        // GET: api/IndividualMetrics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IndividualMetric>>> GetIndividualMetrics()
        {
            return await _context.IndividualMetrics.ToListAsync();
        }

        // GET: api/IndividualMetrics/5
        [HttpGet("{projectId}")]
        public async Task<ActionResult<IndividualMetric>> GetIndividualMetric(int projectId)
        {
            var individualMetric = await _context.IndividualMetrics.FirstOrDefaultAsync(p => p.ProjectId == projectId);

            if (individualMetric == null)
            {
                return NotFound();
            }

            return individualMetric;
        }

        // PUT: api/IndividualMetrics/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{projectId}")]
        public async Task<IActionResult> PutIndividualMetric(int projectId, IndividualMetric individualMetric)
        {
            if (projectId != individualMetric.ProjectId)
            {
                return BadRequest();
            }

            _context.Entry(individualMetric).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IndividualMetricExists(projectId))
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

        // POST: api/IndividualMetrics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IndividualMetric>> PostIndividualMetric(IndividualMetric individualMetric)
        {
            _context.IndividualMetrics.Add(individualMetric);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIndividualMetric", new { projectId = individualMetric.ProjectId }, individualMetric);
        }

        // DELETE: api/IndividualMetrics/5
        [HttpDelete("{projectId}")]
        public async Task<IActionResult> DeleteIndividualMetric(int projectId)
        {
            var individualMetric = await _context.IndividualMetrics.FirstOrDefaultAsync(im => im.ProjectId == projectId);
            if (individualMetric == null)
            {
                return NotFound();
            }

            _context.IndividualMetrics.Remove(individualMetric);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IndividualMetricExists(int projectId)
        {
            return _context.IndividualMetrics.Any(e => e.ProjectId == projectId);
        }
    }
}
