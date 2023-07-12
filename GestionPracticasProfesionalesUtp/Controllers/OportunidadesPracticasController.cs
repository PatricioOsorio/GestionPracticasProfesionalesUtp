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
  public class OportunidadesPracticasController : Controller
  {
    private readonly ApplicationDbContext _context;

    public OportunidadesPracticasController(ApplicationDbContext context)
    {
      _context = context;
    }

    // GET: OportunidadesPracticas
    public async Task<IActionResult> Index()
    {
      var applicationDbContext = _context.OportunidadPracticas.Include(o => o.CoordinadorOrganizacion).Include(o => o.Organizacion);
      return View(await applicationDbContext.ToListAsync());
    }

    // GET: OportunidadesPracticas/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null || _context.OportunidadPracticas == null)
      {
        return NotFound();
      }

      var oportunidadesPracticas = await _context.OportunidadPracticas
          .Include(o => o.CoordinadorOrganizacion)
          .Include(o => o.Organizacion)
          .FirstOrDefaultAsync(m => m.OportunidadPracticaId == id);
      if (oportunidadesPracticas == null)
      {
        return NotFound();
      }

      return View(oportunidadesPracticas);
    }

    // GET: OportunidadesPracticas/Create
    public IActionResult Create()
    {
      ViewData["CoordinadorOrganizacionId"] = new SelectList(_context.CoordinadorOrganizacion, "CoordinadorOrganizacionId", "CoordinadorOrganizacionId");
      ViewData["OrganizacionId"] = new SelectList(_context.Organizaciones, "OrganizacionId", "OrganizacionId");
      return View();
    }

    // POST: OportunidadesPracticas/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("OportunidadPracticaId,OrganizacionId,CoordinadorOrganizacionId,Descripcion,Requisitos,FechaInicio,FechaFin")] OportunidadesPracticas oportunidadesPracticas)
    {
      if (ModelState.IsValid)
      {
        _context.Add(oportunidadesPracticas);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      ViewData["CoordinadorOrganizacionId"] = new SelectList(_context.CoordinadorOrganizacion, "CoordinadorOrganizacionId", "CoordinadorOrganizacionId", oportunidadesPracticas.CoordinadorOrganizacionId);
      ViewData["OrganizacionId"] = new SelectList(_context.Organizaciones, "OrganizacionId", "OrganizacionId", oportunidadesPracticas.OrganizacionId);
      return View(oportunidadesPracticas);
    }

    // GET: OportunidadesPracticas/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null || _context.OportunidadPracticas == null)
      {
        return NotFound();
      }

      var oportunidadesPracticas = await _context.OportunidadPracticas.FindAsync(id);
      if (oportunidadesPracticas == null)
      {
        return NotFound();
      }
      ViewData["CoordinadorOrganizacionId"] = new SelectList(_context.CoordinadorOrganizacion, "CoordinadorOrganizacionId", "CoordinadorOrganizacionId", oportunidadesPracticas.CoordinadorOrganizacionId);
      ViewData["OrganizacionId"] = new SelectList(_context.Organizaciones, "OrganizacionId", "OrganizacionId", oportunidadesPracticas.OrganizacionId);
      return View(oportunidadesPracticas);
    }

    // POST: OportunidadesPracticas/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("OportunidadPracticaId,OrganizacionId,CoordinadorOrganizacionId,Descripcion,Requisitos,FechaInicio,FechaFin")] OportunidadesPracticas oportunidadesPracticas)
    {
      if (id != oportunidadesPracticas.OportunidadPracticaId)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(oportunidadesPracticas);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!OportunidadesPracticasExists(oportunidadesPracticas.OportunidadPracticaId))
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
      ViewData["CoordinadorOrganizacionId"] = new SelectList(_context.CoordinadorOrganizacion, "CoordinadorOrganizacionId", "CoordinadorOrganizacionId", oportunidadesPracticas.CoordinadorOrganizacionId);
      ViewData["OrganizacionId"] = new SelectList(_context.Organizaciones, "OrganizacionId", "OrganizacionId", oportunidadesPracticas.OrganizacionId);
      return View(oportunidadesPracticas);
    }

    // GET: OportunidadesPracticas/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null || _context.OportunidadPracticas == null)
      {
        return NotFound();
      }

      var oportunidadesPracticas = await _context.OportunidadPracticas
          .Include(o => o.CoordinadorOrganizacion)
          .Include(o => o.Organizacion)
          .FirstOrDefaultAsync(m => m.OportunidadPracticaId == id);
      if (oportunidadesPracticas == null)
      {
        return NotFound();
      }

      return View(oportunidadesPracticas);
    }

    // POST: OportunidadesPracticas/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      if (_context.OportunidadPracticas == null)
      {
        return Problem("Entity set 'ApplicationDbContext.OportunidadPracticas'  is null.");
      }
      var oportunidadesPracticas = await _context.OportunidadPracticas.FindAsync(id);
      if (oportunidadesPracticas != null)
      {
        _context.OportunidadPracticas.Remove(oportunidadesPracticas);
      }

      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool OportunidadesPracticasExists(int id)
    {
      return (_context.OportunidadPracticas?.Any(e => e.OportunidadPracticaId == id)).GetValueOrDefault();
    }
  }
}
