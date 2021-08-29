using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectsApi.Models;

namespace ProjectsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalMetricsController : ControllerBase
    {
        private readonly ProjectsDbContext _context;

        public GlobalMetricsController(ProjectsDbContext context)
        {
            _context = context;            
        }

        [HttpGet]
        public async Task<ActionResult<GlobalMetric>> GetGlobalMetrics()
        {
            return await _context.GlobalMetrics.FirstOrDefaultAsync();
        }

        [HttpPut]
        public async Task<IActionResult> PutGlobalMetric(GlobalMetric globalMetric)
        {
            if (globalMetric.Id != 1)
            {
                return BadRequest();
            }

            _context.Entry(globalMetric).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GlobalMetricExists(globalMetric.Id))
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

        private bool GlobalMetricExists(int id)
        {
            return _context.GlobalMetrics.Any(e => e.Id == id);
        }
    }
}
