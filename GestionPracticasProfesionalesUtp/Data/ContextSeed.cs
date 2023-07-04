using GestionPracticasProfesionalesUtp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;

namespace GestionPracticasProfesionalesUtp.Data
{
  public enum Roles
  {
    SUPERADMIN,
    COORDINADOR_PRACTICA,
    ESTUDIANTE,
    ORGANIZACION
  }

  public static class ContextSeed
  {
    public static async Task SeedRolesAsync(
        UserManager<Users> userManager,
        RoleManager<IdentityRole> roleManager
      )
    {
      //Seed Roles
      await roleManager.CreateAsync(new IdentityRole(Roles.SUPERADMIN.ToString()));
      await roleManager.CreateAsync(new IdentityRole(Roles.COORDINADOR_PRACTICA.ToString()));
      await roleManager.CreateAsync(new IdentityRole(Roles.ESTUDIANTE.ToString()));
      await roleManager.CreateAsync(new IdentityRole(Roles.ORGANIZACION.ToString()));

    }

    public static async Task SeedSuperAdminAsync(
      UserManager<Users> userManager,
      RoleManager<IdentityRole> roleManager
    )
    {
      //Seed Default User
      var newUser = new Users()
      {
        Nombre = "Patricio",
        ApellidoPaterno = "Osorio",
        ApellidoMaterno = "Osorio",
        UserName = "patriciomiguel_12@hotmail.com",
        Email = "patriciomiguel_12@hotmail.com",
        EmailConfirmed = true,
      };
      if (userManager.Users.All(u => u.Id != newUser.Id))
      {
        var user = await userManager.FindByEmailAsync(newUser.Email);
        if (user == null)
        {
          await userManager.CreateAsync(newUser, "Pato123.");
          await userManager.AddToRoleAsync(newUser, Roles.SUPERADMIN.ToString());
        }

      }
    }

    public static async Task SeedStudentAsync(
      UserManager<Users> userManager,
      RoleManager<IdentityRole> roleManager, ApplicationDbContext context
    )
    {
      //Seed Default User
      var newUser = new Users()
      {
        Nombre = "AlumnoNombre",
        ApellidoPaterno = "AP",
        ApellidoMaterno = "AM",
        UserName = "student@hotmail.com",
        Email = "student@hotmail.com",
        EmailConfirmed = true,
      };
      if (userManager.Users.All(u => u.Id != newUser.Id))
      {
        var user = await userManager.FindByEmailAsync(newUser.Email);
        if (user == null)
        {
          await userManager.CreateAsync(newUser, "Pato123.");
          await userManager.AddToRoleAsync(newUser, Roles.ESTUDIANTE.ToString());

          // Create Student record
          var studentUser = new Students()
          {
            Matricula = "UTP0147941",
            UserId = newUser.Id,
            Carrera = "TI",
            Semestre = "9"
          };

          context.Students.Add(studentUser);
          await context.SaveChangesAsync();
        }

      }
    }

    public static async Task SeedCoordinadorPracticasAsync(
     UserManager<Users> userManager,
     RoleManager<IdentityRole> roleManager, ApplicationDbContext context
   )
    {
      //Seed Default User
      var newUser = new Users()
      {
        Nombre = "Coordinador",
        ApellidoPaterno = "AP",
        ApellidoMaterno = "AM",
        UserName = "coordinador@hotmail.com",
        Email = "coordinador@hotmail.com",
        EmailConfirmed = true,
      };
      if (userManager.Users.All(u => u.Id != newUser.Id))
      {
        var user = await userManager.FindByEmailAsync(newUser.Email);
        if (user == null)
        {
          await userManager.CreateAsync(newUser, "Pato123.");
          await userManager.AddToRoleAsync(newUser, Roles.ESTUDIANTE.ToString());

          // Create Student record
          var coordinadorUser = new CoordinadorPracticas()
          {
            CoordinadorPracticaId = newUser.Id,
            Departamento = "TI",
            Facultad = "Redes"
          };

          context.CoordinadorPracticas.Add(coordinadorUser);
          await context.SaveChangesAsync();
        }

      }
    }

    public static async Task SeedOrganizacionAsync(
       UserManager<Users> userManager,
       RoleManager<IdentityRole> roleManager, ApplicationDbContext context
     )
    {
      // Create Organizacion record
      var newOrganizacion = new Organizaciones()
      {
        //OrganizacionId = newUser.Id,
        Nombre = "TecAsociation",
        Descripcion = "Empresa de tecnologia con ambito en...",
        Direccion = "Puebla, Mexico",
        Telefono = "2222222222",
        Correo = "tecasociation@hotmail.com"
      };

      context.Organizaciones.Add(newOrganizacion);
      await context.SaveChangesAsync();
    }
  }
}