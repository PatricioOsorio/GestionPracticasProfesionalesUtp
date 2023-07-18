using GestionPracticasProfesionalesUtp.Data;
using GestionPracticasProfesionalesUtp.Models;
using GestionPracticasProfesionalesUtp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GestionPracticasProfesionalesUtp.Controllers
{
  [Authorize(Roles = "SUPERADMIN, ORGANIZACION")]
  public class OrganizacionController : Controller
  {
    private readonly UserManager<Users> _userManager;
    private readonly ApplicationDbContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;

    public OrganizacionController(UserManager<Users> userManager, ApplicationDbContext context,
        RoleManager<IdentityRole> roleManager)
    {
      _userManager = userManager;
      _context = context;
      _roleManager = roleManager;
    }
    public async Task<IActionResult> ListCoordinadorOrganizacion()
    {
      var user = await _userManager.GetUserAsync(User);

      if (await _userManager.IsInRoleAsync(user, "SUPERADMIN"))
      {
        var coordinadores = await _context.CoordinadorOrganizacion
            .Include(c => c.User)
            .Include(c => c.Organizacion) // Incluimos la información de la organización
            .ToListAsync();

        return View(coordinadores);
      }
      else if (await _userManager.IsInRoleAsync(user, "ORGANIZACION"))
      {
        var organizacion = await _context.Organizaciones.FirstOrDefaultAsync(o => o.OrganizacionId == user.Id);

        if (organizacion == null)
        {
          // No se encontró la organización asociada al usuario actual
          return NotFound();
        }

        var coordinadores = await _context.CoordinadorOrganizacion
            .Include(c => c.User)
            .Where(c => c.OrganizacionId == organizacion.OrganizacionId)
            .ToListAsync();

        return View(coordinadores);
      }

      // El usuario no tiene el rol SUPERADMIN ni ORGANIZACION
      return Forbid();
    }

    // GET: CreateCoordinadorOrganizacion
    [HttpGet]
    public IActionResult CreateCoordinadorOrganizacion()
    {
      var viewModel = new CreateUserCoordinarOrganizacionViewModel();

      // If the user has the SUPERADMIN role, populate the Organizaciones dropdown
      if (User.IsInRole("SUPERADMIN"))
      {
        var organizaciones = _context.Organizaciones.Select(o => new SelectListItem
        {
          Value = o.OrganizacionId,
          Text = o.NombreOrganizacion
        }).ToList();

        viewModel.Roles = new List<SelectListItem>
                {
                    new SelectListItem { Value = "COORDINADOR_PRACTICA_ORGANIZACION", Text = "Coordinador Práctica Organización" }
                };

        ViewData["Organizaciones"] = organizaciones;
      }
      else if (User.IsInRole("ORGANIZACION"))
      {
        // If the user has the ORGANIZACION role, set the Organization based on the logged-in user
        var organizacion = _context.Organizaciones.FirstOrDefault(o => o.User.UserName == User.Identity.Name);

        if (organizacion != null)
        {
          ViewData["OrganizationId"] = organizacion.OrganizacionId;
        }

        viewModel.Roles = new List<SelectListItem>
                {
                    new SelectListItem { Value = "COORDINADOR_PRACTICA_ORGANIZACION", Text = "Coordinador Práctica Organización" }
                };
      }

      return View(viewModel);
    }

    // POST: CreateCoordinadorOrganizacion
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCoordinadorOrganizacion(CreateUserCoordinarOrganizacionViewModel viewModel)
    {
      if (ModelState.IsValid)
      {
        var user = new Users
        {
          UserName = viewModel.Email,
          Email = viewModel.Email,
          Nombre = viewModel.Nombre,
          ApellidoPaterno = viewModel.ApellidoPaterno,
          ApellidoMaterno = viewModel.ApellidoMaterno
        };

        var result = await _userManager.CreateAsync(user, viewModel.Password);

        if (result.Succeeded)
        {
          await _userManager.AddToRoleAsync(user, "COORDINADOR_PRACTICA_ORGANIZACION");

          // If the user has the SUPERADMIN role, associate with the selected Organization
          if (User.IsInRole("SUPERADMIN") && viewModel.OrganizationId != null)
          {
            var coordinadorUser = new CoordinadorOrganizacion
            {
              CoordinadorOrganizacionId = user.Id,
              Area = viewModel.Area,
              OrganizacionId = viewModel.OrganizationId
            };
            _context.CoordinadorOrganizacion.Add(coordinadorUser);
          }
          else if (User.IsInRole("ORGANIZACION"))
          {
            // If the user has the ORGANIZACION role, associate with their own Organization
            var organizacion = _context.Organizaciones.FirstOrDefault(o => o.User.UserName == User.Identity.Name);
            if (organizacion != null)
            {
              var coordinadorUser = new CoordinadorOrganizacion
              {
                CoordinadorOrganizacionId = user.Id,
                Area = viewModel.Area,
                OrganizacionId = organizacion.OrganizacionId
              };
              _context.CoordinadorOrganizacion.Add(coordinadorUser);
            }
          }

          await _context.SaveChangesAsync();
          return RedirectToAction("ListCoordinadorOrganizacion", "Organizacion");
        }
      }

      // Repopulate the Organizaciones dropdown for SUPERADMIN role
      if (User.IsInRole("SUPERADMIN"))
      {
        var organizaciones = _context.Organizaciones.Select(o => new SelectListItem
        {
          Value = o.OrganizacionId,
          Text = o.NombreOrganizacion
        }).ToList();

        viewModel.Roles = new List<SelectListItem>
        {
            new SelectListItem { Value = "COORDINADOR_PRACTICA_ORGANIZACION", Text = "Coordinador Práctica Organización" }
        };

        ViewData["Organizaciones"] = organizaciones;
      }
      else if (User.IsInRole("ORGANIZACION"))
      {
        // Repopulate the OrganizationId for ORGANIZACION role
        var organizacion = _context.Organizaciones.FirstOrDefault(o => o.User.UserName == User.Identity.Name);

        if (organizacion != null)
        {
          ViewData["OrganizationId"] = organizacion.OrganizacionId;
        }

        viewModel.Roles = new List<SelectListItem>
        {
            new SelectListItem { Value = "COORDINADOR_PRACTICA_ORGANIZACION", Text = "Coordinador Práctica Organización" }
        };
      }

      return View(viewModel);
    }

    // GET: EditCoordinadorOrganizacion
    [HttpGet]
    public async Task<IActionResult> EditCoordinadorOrganizacion(string id)
    {
      var user = await _userManager.FindByIdAsync(id);

      if (user == null || !await _userManager.IsInRoleAsync(user, "COORDINADOR_PRACTICA_ORGANIZACION"))
      {
        return NotFound();
      }

      var viewModel = new EditUserCoordinarOrganizacionViewModel
      {
        UserId = user.Id,
        Email = user.Email,
        Nombre = user.Nombre,
        ApellidoPaterno = user.ApellidoPaterno,
        ApellidoMaterno = user.ApellidoMaterno
      };

      if (User.IsInRole("SUPERADMIN"))
      {
        var organizaciones = _context.Organizaciones.Select(o => new SelectListItem
        {
          Value = o.OrganizacionId,
          Text = o.NombreOrganizacion
        }).ToList();

        viewModel.Roles = new List<SelectListItem>
        {
            new SelectListItem { Value = "COORDINADOR_PRACTICA_ORGANIZACION", Text = "Coordinador Práctica Organización" }
        };

        ViewData["Organizaciones"] = organizaciones;
      }
      else if (User.IsInRole("ORGANIZACION"))
      {
        var organizacion = _context.Organizaciones.FirstOrDefault(o => o.User.UserName == User.Identity.Name);
        if (organizacion != null)
        {
          ViewData["OrganizationId"] = organizacion.OrganizacionId;
        }

        viewModel.Roles = new List<SelectListItem>
        {
            new SelectListItem { Value = "COORDINADOR_PRACTICA_ORGANIZACION", Text = "Coordinador Práctica Organización" }
        };
      }

      return View(viewModel);
    }

    // POST: EditCoordinadorOrganizacion
    [HttpPost]
    [Authorize(Roles = "SUPERADMIN,ORGANIZACION")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditCoordinadorOrganizacion(EditUserCoordinarOrganizacionViewModel viewModel)
    {
      if (ModelState.IsValid)
      {
        var user = await _userManager.FindByIdAsync(viewModel.UserId);

        if (user == null || !await _userManager.IsInRoleAsync(user, "COORDINADOR_PRACTICA_ORGANIZACION"))
        {
          return NotFound();
        }

        user.Email = viewModel.Email;
        user.Nombre = viewModel.Nombre;
        user.ApellidoPaterno = viewModel.ApellidoPaterno;
        user.ApellidoMaterno = viewModel.ApellidoMaterno;

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
          // If the user has the SUPERADMIN role, update the associated Organization
          if (User.IsInRole("SUPERADMIN") && viewModel.OrganizationId != null)
          {
            var coordinador = _context.CoordinadorOrganizacion.FirstOrDefault(c => c.CoordinadorOrganizacionId == viewModel.UserId);
            if (coordinador != null)
            {
              coordinador.Area = viewModel.Area;
              coordinador.OrganizacionId = viewModel.OrganizationId;
              _context.CoordinadorOrganizacion.Update(coordinador);
            }
          }
          else if (User.IsInRole("ORGANIZACION"))
          {
            // If the user has the ORGANIZACION role, update their own Organization
            var organizacion = _context.Organizaciones.FirstOrDefault(o => o.User.UserName == User.Identity.Name);
            if (organizacion != null)
            {
              var coordinador = _context.CoordinadorOrganizacion.FirstOrDefault(c => c.CoordinadorOrganizacionId == viewModel.UserId);
              if (coordinador != null)
              {
                coordinador.Area = viewModel.Area;
                _context.CoordinadorOrganizacion.Update(coordinador);
              }
            }
          }

          await _context.SaveChangesAsync();
          return RedirectToAction("ListCoordinadorOrganizacion", "Organizacion");
        }
      }

      // Repopulate the Organizaciones dropdown for SUPERADMIN role
      if (User.IsInRole("SUPERADMIN"))
      {
        var organizaciones = _context.Organizaciones.Select(o => new SelectListItem
        {
          Value = o.OrganizacionId,
          Text = o.NombreOrganizacion
        }).ToList();

        viewModel.Roles = new List<SelectListItem>
        {
            new SelectListItem { Value = "COORDINADOR_PRACTICA_ORGANIZACION", Text = "Coordinador Práctica Organización" }
        };

        ViewData["Organizaciones"] = organizaciones;
      }
      else if (User.IsInRole("ORGANIZACION"))
      {
        // Repopulate the OrganizationId for ORGANIZACION role
        var organizacion = _context.Organizaciones.FirstOrDefault(o => o.User.UserName == User.Identity.Name);

        if (organizacion != null)
        {
          ViewData["OrganizationId"] = organizacion.OrganizacionId;
        }

        viewModel.Roles = new List<SelectListItem>
        {
            new SelectListItem { Value = "COORDINADOR_PRACTICA_ORGANIZACION", Text = "Coordinador Práctica Organización" }
        };
      }

      return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUser(string id)
    {
      Users user = await _userManager.FindByIdAsync(id);

      if (user != null)
      {
        IdentityResult result = await _userManager.DeleteAsync(user);

        if (result.Succeeded)
        {
          return RedirectToAction("ListCoordinadorOrganizacion");
        }

        foreach (IdentityError error in result.Errors)
        {
          ModelState.AddModelError(string.Empty, error.Description);
        }
      }

      return NotFound();
    }

    // POST: DeleteCoordinadorOrganizacion
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCoordinadorOrganizacion(EditUserCoordinarOrganizacionViewModel viewModel)
    {
      if (ModelState.IsValid)
      {
        var user = await _userManager.FindByIdAsync(viewModel.UserId);
        if (user == null)
        {
          return NotFound();
        }

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
          return RedirectToAction("ListCoordinadorOrganizacion", "Organizacion");
        }
      }

      return View(viewModel);
    }

  }
}

