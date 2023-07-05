//using GestionPracticasProfesionalesUtp.Models;
using GestionPracticasProfesionalesUtp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

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

      //Relación 1 a 1 entre Users y Students
      builder.Entity<Students>()
        .HasOne(s => s.User)
        .WithOne(u => u.Student)
        .HasForeignKey<Students>(s => s.UserId);

      //Relación 1 a 1 entre Users y Students
      builder.Entity<CoordinadorPracticas>()
        .HasOne(c => c.User)
        .WithOne(u => u.CoordinadorPractica)
        .HasForeignKey<CoordinadorPracticas>(c => c.CoordinadorPracticaId);


      // Estblece autoincremental el id de Organizaciones
      builder.Entity<Organizaciones>()
        .Property(o => o.OrganizacionId)
        .ValueGeneratedOnAdd();

      builder.Entity<OportunidadPracticas>()
          .HasOne(op => op.Organizacion)
          .WithMany(o => o.OportunidadPracticas)
          .HasForeignKey(op => op.OrganizacionId);
    }

    public DbSet<Students> Students { get; set; }
    public DbSet<CoordinadorPracticas> CoordinadorPracticas { get; set; }
    public DbSet<Organizaciones> Organizaciones { get; set; }
    public DbSet<OportunidadPracticas> OportunidadPracticas { get; set; }
  }
}