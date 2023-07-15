using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionPracticasProfesionalesUtp.Data;
using GestionPracticasProfesionalesUtp.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace GestionPracticasProfesionalesUtp.Controllers
{

  [Authorize(Roles = "SUPERADMIN, ORGANIZACION")]

  public class OportunidadesPracticasController : Controller
  {
    private readonly ApplicationDbContext _context;
    private readonly UserManager<Users> _userManager;

    public OportunidadesPracticasController(ApplicationDbContext context, UserManager<Users> userManager)
    {
      _context = context;
      _userManager = userManager;
    }


    public async Task<IActionResult> Index()
    {
      // Verificar el rol del usuario actual
      var user = await _userManager.GetUserAsync(User);
      var roles = await _userManager.GetRolesAsync(user);

      // Obtener el ID de la organización asociada al usuario actual, si corresponde
      string organizacionId = null;
      if (roles.Contains("ORGANIZACION"))
      {
        var organizacion = await _context.Organizaciones.FirstOrDefaultAsync(o => o.User.Id == user.Id);
        if (organizacion != null)
        {
          organizacionId = organizacion.OrganizacionId;
        }
      }

      // Filtrar los registros de oportunidades de prácticas según el rol del usuario
      IQueryable<OportunidadesPracticas> oportunidadesQuery = _context.OportunidadPracticas
          .Include(o => o.CoordinadorOrganizacion)
          .Include(o => o.Organizacion)
          .Include(o => o.CoordinadorOrganizacion.User);

      if (roles.Contains("SUPERADMIN"))
      {
        // Mostrar todos los registros para el rol SUPERADMIN
      }
      else if (roles.Contains("ORGANIZACION"))
      {
        // Mostrar los registros de la organización asociada al usuario
        oportunidadesQuery = oportunidadesQuery.Where(o => o.OrganizacionId == organizacionId);
      }
      else
      {
        // Si el usuario no tiene los roles permitidos, redirigir a una página de acceso denegado u otra acción adecuada
        return RedirectToAction("AccessDenied", "Account");
      }

      // Pasar los registros filtrados a la vista
      var oportunidades = await oportunidadesQuery.ToListAsync();
      return View(oportunidades);
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
      if (User.IsInRole("SUPERADMIN"))
      {
        ViewData["OrganizacionId"] = new SelectList(_context.Organizaciones, "OrganizacionId", "NombreOrganizacion");
        ViewData["CoordinadorOrganizacionId"] = new SelectList(_context.CoordinadorOrganizacion, "CoordinadorOrganizacionId", "User.Nombre");
      }
      else if (User.IsInRole("ORGANIZACION"))
      {
        var organizacion = _context.Organizaciones.FirstOrDefault(o => o.User.UserName == User.Identity.Name);
        if (organizacion != null)
        {
          ViewData["OrganizacionId"] = organizacion.OrganizacionId;
          ViewData["CoordinadorOrganizacionId"] = new SelectList(_context.CoordinadorOrganizacion.Where(co => co.OrganizacionId == organizacion.OrganizacionId), "CoordinadorOrganizacionId", "User.Nombre");
        }
      }
      return View();
    }

    [HttpGet]
    public IActionResult GetCoordinadores(string organizacionId)
    {
      var coordinadores = _context.CoordinadorOrganizacion
          .Where(co => co.OrganizacionId == organizacionId)
          .Select(co => new
          {
            coordinadorOrganizacionId = co.CoordinadorOrganizacionId,
            nombre = co.User.Nombre + " " + co.User.ApellidoPaterno + " " + co.User.ApellidoMaterno
          })
          .ToList();

      return Json(coordinadores);
    }

    // POST: OportunidadesPracticas/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("OportunidadPracticaId,OrganizacionId,CoordinadorOrganizacionId,Descripcion,Requisitos,FechaInicio,FechaFin")] OportunidadesPracticas oportunidadesPracticas)
    {
      if (ModelState.IsValid)
      {
        // Obtener la organización y el coordinador seleccionados
        var organizacion = await _context.Organizaciones.FindAsync(oportunidadesPracticas.OrganizacionId);
        var coordinador = await _context.CoordinadorOrganizacion.FindAsync(oportunidadesPracticas.CoordinadorOrganizacionId);

        if (organizacion != null && coordinador != null)
        {
          // Asociar la oportunidad de prácticas con la organización y el coordinador correspondientes
          oportunidadesPracticas.Organizacion = organizacion;
          oportunidadesPracticas.CoordinadorOrganizacion = coordinador;

          // Agregar la oportunidad de prácticas al contexto
          _context.OportunidadPracticas.Add(oportunidadesPracticas);

          // Guardar los cambios en la base de datos
          await _context.SaveChangesAsync();

          return RedirectToAction(nameof(Index));
        }
      }

      // Si se llega a este punto, ocurrió un error en la validación o en la asociación de la oportunidad de prácticas
      // Vuelve a cargar la vista con los datos ingresados por el usuario y muestra los mensajes de error correspondientes
      if (User.IsInRole("SUPERADMIN"))
      {
        ViewData["OrganizacionId"] = new SelectList(_context.Organizaciones, "OrganizacionId", "NombreOrganizacion");
        ViewData["CoordinadorOrganizacionId"] = new SelectList(_context.CoordinadorOrganizacion, "CoordinadorOrganizacionId", "User.Nombre");
      }
      else if (User.IsInRole("ORGANIZACION"))
      {
        var organizacion = await _context.Organizaciones.FirstOrDefaultAsync(o => o.User.UserName == User.Identity.Name);
        if (organizacion != null)
        {
          ViewData["OrganizacionId"] = organizacion.OrganizacionId;
          ViewData["CoordinadorOrganizacionId"] = new SelectList(_context.CoordinadorOrganizacion.Where(co => co.OrganizacionId == organizacion.OrganizacionId), "CoordinadorOrganizacionId", "User.Nombre");
        }
      }

      return View(oportunidadesPracticas);
    }


    // GET: OportunidadesPracticas/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var oportunidadPracticas = await _context.OportunidadPracticas.FindAsync(id);
      if (oportunidadPracticas == null)
      {
        return NotFound();
      }

      if (User.IsInRole("SUPERADMIN"))
      {
        ViewData["OrganizacionId"] = new SelectList(_context.Organizaciones, "OrganizacionId", "NombreOrganizacion", oportunidadPracticas.OrganizacionId);
        ViewData["CoordinadorOrganizacionId"] = new SelectList(_context.CoordinadorOrganizacion, "CoordinadorOrganizacionId", "User.Nombre", oportunidadPracticas.CoordinadorOrganizacionId);
      }
      else if (User.IsInRole("ORGANIZACION"))
      {
        var organizacion = await _context.Organizaciones.FirstOrDefaultAsync(o => o.User.UserName == User.Identity.Name);
        if (organizacion != null)
        {
          ViewData["OrganizacionId"] = organizacion.OrganizacionId;
          ViewData["CoordinadorOrganizacionId"] = new SelectList(_context.CoordinadorOrganizacion.Where(co => co.OrganizacionId == organizacion.OrganizacionId), "CoordinadorOrganizacionId", "User.Nombre", oportunidadPracticas.CoordinadorOrganizacionId);
        }
      }

      return View(oportunidadPracticas);
    }

    // POST: OportunidadesPracticas/Edit/5
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
          var organizacion = await _context.Organizaciones.FindAsync(oportunidadesPracticas.OrganizacionId);
          var coordinador = await _context.CoordinadorOrganizacion.FindAsync(oportunidadesPracticas.CoordinadorOrganizacionId);

          if (organizacion != null && coordinador != null)
          {
            oportunidadesPracticas.Organizacion = organizacion;
            oportunidadesPracticas.CoordinadorOrganizacion = coordinador;

            _context.Update(oportunidadesPracticas);
            await _context.SaveChangesAsync();
          }
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

      if (User.IsInRole("SUPERADMIN"))
      {
        ViewData["OrganizacionId"] = new SelectList(_context.Organizaciones, "OrganizacionId", "NombreOrganizacion", oportunidadesPracticas.OrganizacionId);
        ViewData["CoordinadorOrganizacionId"] = new SelectList(_context.CoordinadorOrganizacion, "CoordinadorOrganizacionId", "User.Nombre", oportunidadesPracticas.CoordinadorOrganizacionId);
      }
      else if (User.IsInRole("ORGANIZACION"))
      {
        var organizacion = await _context.Organizaciones.FirstOrDefaultAsync(o => o.User.UserName == User.Identity.Name);
        if (organizacion != null)
        {
          ViewData["OrganizacionId"] = organizacion.OrganizacionId;
          ViewData["CoordinadorOrganizacionId"] = new SelectList(_context.CoordinadorOrganizacion.Where(co => co.OrganizacionId == organizacion.OrganizacionId), "CoordinadorOrganizacionId", "User.Nombre", oportunidadesPracticas.CoordinadorOrganizacionId);
        }
      }

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
        return Problem("Entity set 'ApplicationDbContext.OportunidadPracticas' is null.");
      }

      var oportunidadesPracticas = await _context.OportunidadPracticas.FindAsync(id);

      if (oportunidadesPracticas != null)
      {
        _context.OportunidadPracticas.Remove(oportunidadesPracticas);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
      }

      return NotFound();
    }

    private bool OportunidadesPracticasExists(int id)
    {
      return (_context.OportunidadPracticas?.Any(e => e.OportunidadPracticaId == id)).GetValueOrDefault();
    }
  }
}
