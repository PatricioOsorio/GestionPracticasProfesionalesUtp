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
    // Read[GET] - Roles
    public async Task<IActionResult> ReadRoles()
    {
      var roles = _roleManager.Roles.ToList();
      return View(roles);
    }

    // Read[GET] - Roles/CreateRole
    public IActionResult CreateRole()
    {
      return View();
    }

    // Update[POST] - Roles/CreateRole
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateRole([Bind("Name")] IdentityRole role)
    {
      if (ModelState.IsValid)
      {
        var result = await _roleManager.CreateAsync(role);
        if (result.Succeeded)
        {
          return RedirectToAction(nameof(ReadRoles));
        }
        foreach (var error in result.Errors)
        {
          ModelState.AddModelError(string.Empty, error.Description);
        }
      }
      return View(role);
    }

    // Read[GET] - Roles/UpdateRole/5
    public async Task<IActionResult> UpdateRole(string id)
    {
      if (string.IsNullOrEmpty(id))
      {
        return NotFound();
      }

      var role = await _roleManager.FindByIdAsync(id);
      if (role == null)
      {
        return NotFound();
      }
      return View(role);
    }

    // Update[POST]: Roles/UpdateRole/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateRole(string id, [Bind("Id,Name,ConcurrencyStamp")] IdentityRole role)
    {
      if (id != role.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        var result = await _roleManager.UpdateAsync(role);
        if (result.Succeeded)
        {
          return RedirectToAction(nameof(ReadRoles));
        }
        foreach (var error in result.Errors)
        {
          ModelState.AddModelError(string.Empty, error.Description);
        }
      }
      return View(role);
    }

    // Delete[GET] - User/DeleteUser
    public async Task<IActionResult> DeleteRoleAsync(string id)
    {
      if (string.IsNullOrEmpty(id))
      {
        return NotFound();
      }

      var role = await _roleManager.FindByIdAsync(id);
      if (role == null)
      {
        return NotFound();
      }

      return View(role);
    }

    // Delete[POST]: Roles/Eliminar/5
    [HttpPost]
    public async Task<IActionResult> DeleteRole(string id)
    {
      var role = await _roleManager.FindByIdAsync(id);
      if (role == null)
      {
        return NotFound();
      }

      var result = await _roleManager.DeleteAsync(role);
      if (result.Succeeded)
      {
        return RedirectToAction(nameof(ReadRoles));
      }

      foreach (var error in result.Errors)
      {
        ModelState.AddModelError(string.Empty, error.Description);
      }

      return View(role);
    }

    // ==================================
    // ADMINISTRACION DE LOS USUARIOS
    // ==================================
    // Read[GET] - User
    public async Task<IActionResult> IndexAsync()
    {
      return View(await _userManager.Users.ToListAsync());
    }

    // Update[GET] - User/UpdateUser
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

    // Update[POST] - User/UpdateUser/id
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateUser(string id, Users user, string rol)
    {
      if (id != user.Id)
      {
        return NotFound();
      }

      var usuario = await _userManager.FindByIdAsync(user.Id);

      usuario.Id = user.Id;
      usuario.Email = user.Email;
      usuario.PhoneNumber = user.PhoneNumber;
      usuario.Nombre = user.Nombre;
      usuario.ApellidoPaterno = user.ApellidoPaterno;
      usuario.ApellidoMaterno = user.ApellidoMaterno;

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

    // Delete[GET] - User/DeleteUser
    public IActionResult DeleteUser()
    {
      return View();
    }

    // Delete[POST] - User/DeleteUser/id
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
