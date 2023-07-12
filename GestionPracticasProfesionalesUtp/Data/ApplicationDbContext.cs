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

      builder.Entity<OportunidadesPracticas>()
        .HasOne(op => op.Organizacion)
        .WithMany(o => o.OportunidadPracticas)
        .HasForeignKey(op => op.OrganizacionId)
        .OnDelete(DeleteBehavior.Restrict);

      builder.Entity<OportunidadesPracticas>()
          .HasOne(op => op.CoordinadorOrganizacion)
          .WithMany(co => co.OportunidadPracticas)
          .HasForeignKey(op => op.CoordinadorOrganizacionId)
          .OnDelete(DeleteBehavior.Restrict);

      // Relación 1 a muchos entre CoordinadorOrganizacion y Organizaciones
      builder.Entity<CoordinadorOrganizacion>()
          .HasMany(c => c.Organizaciones)
          .WithOne(o => o.CoordinadorOrganizacion)
          .HasForeignKey(o => o.CoordinadorOrganizacionId);

      // Establecer relación 1 a muchos entre Organizaciones y OportunidadPracticas
      builder.Entity<Organizaciones>()
          .HasMany(o => o.OportunidadPracticas)
          .WithOne(op => op.Organizacion)
          .HasForeignKey(op => op.OrganizacionId);

      // Establecer relación 1 a muchos entre CoordinadorOrganizacion y OportunidadPracticas
      builder.Entity<CoordinadorOrganizacion>()
          .HasMany(co => co.OportunidadPracticas)
          .WithOne(op => op.CoordinadorOrganizacion)
          .HasForeignKey(op => op.CoordinadorOrganizacionId);
    }

    public DbSet<Students> Students { get; set; }
    public DbSet<CoordinadorPracticas> CoordinadorPracticas { get; set; }
    public DbSet<OportunidadesPracticas> OportunidadPracticas { get; set; }
    public DbSet<Organizaciones> Organizaciones { get; set; }
    public DbSet<CoordinadorOrganizacion> CoordinadorOrganizacion { get; set; }

  }
}