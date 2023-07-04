using GestionPracticasProfesionalesUtp.Data;
using GestionPracticasProfesionalesUtp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionPracticasProfesionalesUtp.Controllers
{
  public class TeacherController : Controller
  {
    private readonly UserManager<Users> _userManager;
    private readonly ApplicationDbContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;

    public TeacherController(UserManager<Users> userManager, ApplicationDbContext context,
      RoleManager<IdentityRole> roleManager)
    {
      _userManager = userManager;
      _context = context;
      _roleManager = roleManager;
    }

    // Create student
    public IActionResult CreateStudent()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudent(Students student)
    {
      if (ModelState.IsValid)
      {
        _context.Add(student);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }

      return View(student);
    }

    // Read student
    public async Task<IActionResult> IndexAsync()
    {
      return View(await _context.Students.ToListAsync());
    }

    // Update student
    public async Task<IActionResult> UpdateStudent(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var student = await _context.Students.FindAsync(id);
      if (student == null)
      {
        return NotFound();
      }
      return View(student);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStudent(int id, Students student)
    {
      if (id != student.StudentId)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(student);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound();
        }
        return RedirectToAction(nameof(Index));
      }
      return View(student);
    }

    // Delete student
    public IActionResult DeleteStudent()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteStudent(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var student = await _context.Students.FirstOrDefaultAsync(m => m.StudentId == id);
      if (student == null)
      {
        return NotFound();
      }
      _context.Students.Remove(student);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
  }
}
