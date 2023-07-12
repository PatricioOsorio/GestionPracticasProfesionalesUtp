using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionPracticasProfesionalesUtp.Data;
using GestionPracticasProfesionalesUtp.Models;
using Microsoft.AspNetCore.Authorization;

namespace GestionPracticasProfesionalesUtp.Controllers
{
  [Authorize(Roles = "SUPERADMIN, ORGANIZACION")]

  public class OrganizacionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrganizacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Organizaciones
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Organizaciones.Include(o => o.CoordinadorOrganizacion).Include(o => o.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Organizaciones/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Organizaciones == null)
            {
                return NotFound();
            }

            var organizaciones = await _context.Organizaciones
                .Include(o => o.CoordinadorOrganizacion)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrganizacionId == id);
            if (organizaciones == null)
            {
                return NotFound();
            }

            return View(organizaciones);
        }

        // GET: Organizaciones/Create
        public IActionResult Create()
        {
            ViewData["CoordinadorOrganizacionId"] = new SelectList(_context.CoordinadorOrganizacion, "CoordinadorOrganizacionId", "CoordinadorOrganizacionId");
            ViewData["OrganizacionId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Organizaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrganizacionId,NombreOrganizacion,Descripcion,Direccion,CoordinadorOrganizacionId")] Organizaciones organizaciones)
        {
            if (ModelState.IsValid)
            {
                _context.Add(organizaciones);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoordinadorOrganizacionId"] = new SelectList(_context.CoordinadorOrganizacion, "CoordinadorOrganizacionId", "CoordinadorOrganizacionId", organizaciones.CoordinadorOrganizacionId);
            ViewData["OrganizacionId"] = new SelectList(_context.Users, "Id", "Id", organizaciones.OrganizacionId);
            return View(organizaciones);
        }

        // GET: Organizaciones/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Organizaciones == null)
            {
                return NotFound();
            }

            var organizaciones = await _context.Organizaciones.FindAsync(id);
            if (organizaciones == null)
            {
                return NotFound();
            }
            ViewData["CoordinadorOrganizacionId"] = new SelectList(_context.CoordinadorOrganizacion, "CoordinadorOrganizacionId", "CoordinadorOrganizacionId", organizaciones.CoordinadorOrganizacionId);
            ViewData["OrganizacionId"] = new SelectList(_context.Users, "Id", "Id", organizaciones.OrganizacionId);
            return View(organizaciones);
        }

        // POST: Organizaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("OrganizacionId,NombreOrganizacion,Descripcion,Direccion,CoordinadorOrganizacionId")] Organizaciones organizaciones)
        {
            if (id != organizaciones.OrganizacionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(organizaciones);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganizacionesExists(organizaciones.OrganizacionId))
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
            ViewData["CoordinadorOrganizacionId"] = new SelectList(_context.CoordinadorOrganizacion, "CoordinadorOrganizacionId", "CoordinadorOrganizacionId", organizaciones.CoordinadorOrganizacionId);
            ViewData["OrganizacionId"] = new SelectList(_context.Users, "Id", "Id", organizaciones.OrganizacionId);
            return View(organizaciones);
        }

        // GET: Organizaciones/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Organizaciones == null)
            {
                return NotFound();
            }

            var organizaciones = await _context.Organizaciones
                .Include(o => o.CoordinadorOrganizacion)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrganizacionId == id);
            if (organizaciones == null)
            {
                return NotFound();
            }

            return View(organizaciones);
        }

        // POST: Organizaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Organizaciones == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Organizaciones'  is null.");
            }
            var organizaciones = await _context.Organizaciones.FindAsync(id);
            if (organizaciones != null)
            {
                _context.Organizaciones.Remove(organizaciones);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrganizacionesExists(string id)
        {
          return (_context.Organizaciones?.Any(e => e.OrganizacionId == id)).GetValueOrDefault();
        }
    }
}
