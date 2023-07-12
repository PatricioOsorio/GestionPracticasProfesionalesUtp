using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GestionPracticasProfesionalesUtp.Models;
using System.Diagnostics;
using GestionPracticasProfesionalesUtp.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionPracticasProfesionalesUtp.Controllers
{
  [AllowAnonymous]
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<Users> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, UserManager<Users> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
    {
      _logger = logger;
      _userManager = userManager;
      _roleManager = roleManager;
      _context = context;
    }

    public IActionResult IndexAsync()
    {
      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    public async Task<IActionResult> Publicaciones()
    {
      var applicationDbContext = _context.OportunidadPracticas.Include(o => o.CoordinadorOrganizacion).Include(o => o.Organizacion);
      return View(await applicationDbContext.ToListAsync());
    }
  

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}