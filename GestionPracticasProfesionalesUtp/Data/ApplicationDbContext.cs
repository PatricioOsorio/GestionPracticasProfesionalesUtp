//using GestionPracticasProfesionalesUtp.Models;
using GestionPracticasProfesionalesUtp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionPracticasProfesionalesUtp.Data
{
  public class ApplicationDbContext : IdentityDbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Usar modelos
    public DbSet<IdentityUser> User { get; set; }
    public DbSet<Student> Students { get; set; }
  }
}