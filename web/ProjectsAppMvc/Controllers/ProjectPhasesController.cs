using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectsAppMvc.Models;

namespace ProjectsAppMvc.Controllers
{
    public class ProjectPhasesController : Controller
    {
        private readonly ProjectsDbContext _context;

        public ProjectPhasesController(ProjectsDbContext context)
        {
            _context = context;
        }

        // GET: ProjectPhases
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProjectPhases.ToListAsync());
        }

        // GET: ProjectPhases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectPhase = await _context.ProjectPhases
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectPhase == null)
            {
                return NotFound();
            }

            return View(projectPhase);
        }

        // GET: ProjectPhases/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProjectPhases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProjectId,Name,Status")] ProjectPhase projectPhase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectPhase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(projectPhase);
        }

        // GET: ProjectPhases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectPhase = await _context.ProjectPhases.FindAsync(id);
            if (projectPhase == null)
            {
                return NotFound();
            }
            return View(projectPhase);
        }

        // POST: ProjectPhases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProjectId,Name,Status")] ProjectPhase projectPhase)
        {
            if (id != projectPhase.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectPhase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectPhaseExists(projectPhase.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(projectPhase);
        }

        // GET: ProjectPhases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectPhase = await _context.ProjectPhases
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectPhase == null)
            {
                return NotFound();
            }

            return View(projectPhase);
        }

        // POST: ProjectPhases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectPhase = await _context.ProjectPhases.FindAsync(id);
            _context.ProjectPhases.Remove(projectPhase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectPhaseExists(int id)
        {
            return _context.ProjectPhases.Any(e => e.Id == id);
        }
    }
}
