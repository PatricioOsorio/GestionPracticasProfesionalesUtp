using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GestionPracticasProfesionalesUtp.Data;
using GestionPracticasProfesionalesUtp.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionPracticasProfesionalesUtp.Controllers
{
  [Authorize(Roles = "SUPERADMIN")]
  public class SuperadminController : Controller
  {
    private readonly UserManager<Users> _userManager;
    private readonly ApplicationDbContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;

    public SuperadminController(UserManager<Users> userManager, ApplicationDbContext context,
        RoleManager<IdentityRole> roleManager)
    {
      _userManager = userManager;
      _context = context;
      _roleManager = roleManager;
    }
    // ==================================
    // ADMINISTRACION DE ROLES
    // ==================================
    public async Task<IActionResult> ReadRoles()
    {
      return View();
    }

    // ==================================
    // ADMINISTRACION DE LOS USUARIOS
    // ==================================
    // Read - User
    public async Task<IActionResult> IndexAsync()
    {
      return View(await _userManager.Users.ToListAsync());
    }

    //Update - User
    public async Task<IActionResult> UpdateUser(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var user = await _userManager.FindByIdAsync(id);
      if (user == null)
      {
        return NotFound();
      }

      IList<string> role = await _userManager.GetRolesAsync(user);
      string role1 = role.FirstOrDefault().ToString();

      ViewBag.UserRol = role1;

      return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateUser(string id, IdentityUser user, string rol)
    {
      if (id != user.Id)
      {
        return NotFound();
      }

      var usuario = await _userManager.FindByIdAsync(user.Id);

      usuario.Id = user.Id;
      usuario.Email = user.Email;
      usuario.PhoneNumber = user.PhoneNumber;
      usuario.EmailConfirmed = user.EmailConfirmed;

      var result = await _userManager.UpdateAsync(usuario);
      if (result.Succeeded)
      {
        IList<string> role = await _userManager.GetRolesAsync(usuario);
        string role1 = role.FirstOrDefault().ToString();

        if (rol == null)
          rol = role1;

        var result1 = await _userManager.RemoveFromRoleAsync(usuario, role1);
        if (result1.Succeeded)
        {
          var result2 = await _userManager.AddToRoleAsync(usuario, rol);
          if (result2.Succeeded)
            return RedirectToAction(nameof(Index));
        }
      }

      return View(user);
    }

    // Delete - User
    public IActionResult DeleteUser()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUser(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var user = await _userManager.Users
          .FirstOrDefaultAsync(u => u.Id == id);
      if (user == null)
      {
        return NotFound();
      }

      var result = await _userManager.DeleteAsync(user);
      if (result.Succeeded)
      {
        return RedirectToAction(nameof(Index));
      }

      return RedirectToAction(nameof(Index));
    }
  }
}
