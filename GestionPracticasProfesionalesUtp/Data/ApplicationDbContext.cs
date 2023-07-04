//using GestionPracticasProfesionalesUtp.Models;
using GestionPracticasProfesionalesUtp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionPracticasProfesionalesUtp.Data
{
  public class ApplicationDbContext : IdentityDbContext<Users>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
    }

    // Usar modelos
    //public DbSet<Users> Users { get; set; }
    public DbSet<Students> Students { get; set; }
  }
}