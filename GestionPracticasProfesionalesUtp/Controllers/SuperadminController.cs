using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GestionPracticasProfesionalesUtp.Data;
using GestionPracticasProfesionalesUtp.Models;
using Microsoft.EntityFrameworkCore;
using GestionPracticasProfesionalesUtp.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

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
    //public async Task<IActionResult> DeleteRoleAs(string id)
    //{
    //  if (string.IsNullOrEmpty(id))
    //  {
    //    return NotFound();
    //  }

    //  var role = await _roleManager.FindByIdAsync(id);
    //  if (role == null)
    //  {
    //    return NotFound();
    //  }

    //  return View(role);
    //}

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
    public async Task<IActionResult> ReadUsers()
    {
      return View(await _userManager.Users.ToListAsync());
    }

    // Mostrar el formulario para crear un nuevo usuario
    public async Task<IActionResult> CreateUser()
    {
      // Obtener la lista de roles para mostrar en la vista
      List<IdentityRole> roles = await _roleManager.Roles.ToListAsync();

      CreateUserViewModel model = new CreateUserViewModel
      {
        Roles = roles.Select(r => new SelectListItem { Value = r.Id, Text = r.Name })
      };

      return View("CreateUser", model);
    }

    // Guardar un nuevo usuario
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateUser(CreateUserViewModel model)
    {
      if (ModelState.IsValid)
      {
        Users user = new Users
        {
          Email = model.Email,
          UserName = model.Email,
          Nombre = model.Nombre,
          ApellidoPaterno = model.ApellidoPaterno,
          ApellidoMaterno = model.ApellidoMaterno,
          EmailConfirmed = true,
        };

        IdentityResult result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
          // Asignar el rol seleccionado al nuevo usuario
          IdentityRole role = await _roleManager.FindByIdAsync(model.RoleId);
          if (role != null)
          {
            await _userManager.AddToRoleAsync(user, role.Name);
          }

          return RedirectToAction("ReadUsers");
        }

        foreach (IdentityError error in result.Errors)
        {
          ModelState.AddModelError(string.Empty, error.Description);
        }
      }

      // Obtener la lista de roles para volver a mostrarla en caso de error
      List<IdentityRole> roles = await _roleManager.Roles.ToListAsync();
      model.Roles = roles.Select(r => new SelectListItem { Value = r.Id, Text = r.Name });

      return View(model);
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
          return RedirectToAction("ReadUsers");
        }

        foreach (IdentityError error in result.Errors)
        {
          ModelState.AddModelError(string.Empty, error.Description);
        }
      }

      return NotFound();
    }

    // Mostrar formulario para editar un usuario
    public async Task<IActionResult> UpdateUser(string id)
    {
      Users user = await _userManager.FindByIdAsync(id);

      string selectedRoleId = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

      if (user != null)
      {
        // Obtener la lista de roles para mostrarla en la vista
        List<IdentityRole> roles = await _roleManager.Roles.ToListAsync();
        var model = new UpdateUserViewModel
        {
          Id = user.Id,
          Email = user.Email,
          Nombre = user.Nombre,
          ApellidoPaterno = user.ApellidoPaterno,
          ApellidoMaterno = user.ApellidoMaterno,
          Roles = roles.Select(r => new SelectListItem { Value = r.Id, Text = r.Name }),
          SelectedRoleId = selectedRoleId // Obtener el rol actual del usuario
        };

        return View(model);
      }

      return NotFound();
    }

    // Actualizar un usuario
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateUser(UpdateUserViewModel viewModel)
    {
      if (ModelState.IsValid)
      {
        Users user = await _userManager.FindByIdAsync(viewModel.Id);

        if (user != null)
        {
          user.Email = viewModel.Email;
          user.UserName = viewModel.Email;
          user.Nombre = viewModel.Nombre;
          user.ApellidoPaterno = viewModel.ApellidoPaterno;
          user.ApellidoMaterno = viewModel.ApellidoMaterno;

          // Actualizar el usuario
          IdentityResult result = await _userManager.UpdateAsync(user);

          if (result.Succeeded)
          {
            // Asignar el nuevo rol al usuario
            IdentityRole role = await _roleManager.FindByIdAsync(viewModel.RoleId);
            if (role != null)
            {
              // Remover los roles existentes del usuario
              await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));

              // Asignar el nuevo rol al usuario
              await _userManager.AddToRoleAsync(user, role.Name);
            }

            return RedirectToAction("ReadUsers");
          }

          foreach (IdentityError error in result.Errors)
          {
            ModelState.AddModelError(string.Empty, error.Description);
          }
        }
      }

      // Obtener la lista de roles nuevamente para volver a mostrarla en caso de error
      List<IdentityRole> roles = await _roleManager.Roles.ToListAsync();
      viewModel.Roles = roles.Select(r => new SelectListItem { Value = r.Id, Text = r.Name });

      return View(viewModel);
    }

    public IActionResult Dashboard()
    {
      return View();
    }
  }
}
