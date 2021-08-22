using GeographiesApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeographiesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrativeDivisionLevelsController : ControllerBase
    {
        private readonly GeographiesContext _context;

        public AdministrativeDivisionLevelsController(GeographiesContext context)
        {
            _context = context;
        }

        // GET: api/AdministrativeDivisionLevels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdministrativeDivisionLevel>>> GetAdministrativeDivisionLevels()
        {
            return await _context.AdministrativeDivisionLevels.ToListAsync();
        }
    }
}
