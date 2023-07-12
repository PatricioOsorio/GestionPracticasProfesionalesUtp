using GestionPracticasProfesionalesUtp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Data.OracleClient;

namespace GestionPracticasProfesionalesUtp.Data
{
  public enum Roles
  {
    SUPERADMIN,
    ESTUDIANTE,
    COORDINADOR_PRACTICA_ESCUELA,
    COORDINADOR_PRACTICA_ORGANIZACION,
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
      await roleManager.CreateAsync(new IdentityRole(Roles.ESTUDIANTE.ToString()));
      await roleManager.CreateAsync(new IdentityRole(Roles.COORDINADOR_PRACTICA_ESCUELA.ToString()));
      await roleManager.CreateAsync(new IdentityRole(Roles.COORDINADOR_PRACTICA_ORGANIZACION.ToString()));
      await roleManager.CreateAsync(new IdentityRole(Roles.ORGANIZACION.ToString()));
    }

    public static async Task SeedUserSuperadminAsync(
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

    public static async Task SeedUserStudentAsync(
      UserManager<Users> userManager,
      RoleManager<IdentityRole> roleManager,
      ApplicationDbContext context
    )
    {
      //Seed Default User
      var newUser = new Users()
      {
        Nombre = "Juan",
        ApellidoPaterno = "Perez",
        ApellidoMaterno = "Sanchez",
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

    public static async Task SeedUserCoordinadorPracticaEscuelaAsync(
     UserManager<Users> userManager,
     RoleManager<IdentityRole> roleManager,
     ApplicationDbContext context
    )
        {
          //Seed Default User
          var newUser = new Users()
          {
            Nombre = "Paco",
            ApellidoPaterno = "ApellidoPaterno",
            ApellidoMaterno = "ApellidoMaterno",
            UserName = "coordinador_escuela@hotmail.com",
            Email = "coordinador_escuela@hotmail.com",
            EmailConfirmed = true,
          };
          if (userManager.Users.All(u => u.Id != newUser.Id))
          {
            var user = await userManager.FindByEmailAsync(newUser.Email);
            if (user == null)
            {
              await userManager.CreateAsync(newUser, "Pato123.");
              await userManager.AddToRoleAsync(newUser, Roles.COORDINADOR_PRACTICA_ESCUELA.ToString());

              // Create CoordinadorPracticaEscuela record
              var coordinadorUser = new CoordinadorPracticas()
              {
                CoordinadorPracticaId = newUser.Id,
                Departamento = "TI",
                Facultad = "Redes",
              };

              context.CoordinadorPracticas.Add(coordinadorUser);
              await context.SaveChangesAsync();
            }

          }
        }

    public static async Task SeedUserCoordinadorPracticaOrganizacionAsync(
     UserManager<Users> userManager,
     RoleManager<IdentityRole> roleManager,
     ApplicationDbContext context
    )
    {
      //Seed Default User
      var newUser = new Users()
      {
        Nombre = "Enrique",
        ApellidoPaterno = "ApellidoPaterno",
        ApellidoMaterno = "ApellidoMaterno",
        UserName = "coordinador_organizacion@hotmail.com",
        Email = "coordinador_organizacion@hotmail.com",
        EmailConfirmed = true,
      };
      if (userManager.Users.All(u => u.Id != newUser.Id))
      {
        var user = await userManager.FindByEmailAsync(newUser.Email);
        if (user == null)
        {
          await userManager.CreateAsync(newUser, "Pato123.");
          await userManager.AddToRoleAsync(newUser, Roles.COORDINADOR_PRACTICA_ORGANIZACION.ToString());

          // Create Student record
          var coordinadorUser = new CoordinadorOrganizacion()
          {
            CoordinadorOrganizacionId = newUser.Id,
            Area = "Desarrollo",
          };

          context.CoordinadorOrganizacion.Add(coordinadorUser);
          await context.SaveChangesAsync();
        }

      }
    }

    public static async Task SeedUserOrganizacionAsync(
    UserManager<Users> userManager,
    RoleManager<IdentityRole> roleManager,
    ApplicationDbContext context
    )
        {
          //Seed Default User
          var newUser = new Users()
          {
            Nombre = "Empresa1",
            ApellidoPaterno = "NA",
            ApellidoMaterno = "NA",
            UserName = "empresa@hotmail.com",
            Email = "empresa@hotmail.com",
            EmailConfirmed = true,
          };
          if (userManager.Users.All(u => u.Id != newUser.Id))
          {
            var user = await userManager.FindByEmailAsync(newUser.Email);
            if (user == null)
            {
              await userManager.CreateAsync(newUser, "Pato123.");
              await userManager.AddToRoleAsync(newUser, Roles.ORGANIZACION.ToString());

              // Create Student record
              var coordinadorOrganizacion = await context.CoordinadorOrganizacion.FirstOrDefaultAsync();

              var studentUser = new Organizaciones()
              {
                OrganizacionId = newUser.Id,
                NombreOrganizacion = newUser.Nombre,
                Descripcion = "Esta empresa se encarga de la administracion de tecnoligas emergentes...",
                Direccion = "Puebla, Mexico",
                CoordinadorOrganizacionId = coordinadorOrganizacion.CoordinadorOrganizacionId,
              };

              context.Organizaciones.Add(studentUser);
              await context.SaveChangesAsync();
            }

          }
        }

    //    public static async Task SeedOportunidadPracticasAsync(
    //    UserManager<Users> userManager,
    //    RoleManager<IdentityRole> roleManager,
    //    ApplicationDbContext context
    //)
    //    {
    //      // Obtener la organización existente (puedes ajustar esto según tus necesidades)
    //      var organizacionExistente = await context.Organizaciones.FirstOrDefaultAsync();

    //      if (organizacionExistente != null)
    //      {
    //        // Obtener el coordinador de la organización existente
    //        var coordinadorExistente = await context.CoordinadorOrganizacion
    //            .FirstOrDefaultAsync(c => c.CoordinadorOrganizacionId == organizacionExistente.CoordinadorOrganizacionId);

    //        if (coordinadorExistente != null)
    //        {
    //          // Crear una nueva oportunidad de prácticas
    //          var oportunidadPracticas = new OportunidadesPracticas
    //          {
    //            OrganizacionId = organizacionExistente.OrganizacionId,
    //            CoordinadorOrganizacionId = coordinadorExistente.CoordinadorOrganizacionId,
    //            Descripcion = "Descripción de la oportunidad de prácticas",
    //            Requisitos = "Requisitos para la oportunidad de prácticas",
    //            FechaInicio = DateTime.Now,
    //            FechaFin = DateTime.Now.AddDays(30)
    //          };

    //          // Asociar la oportunidad de prácticas con la organización existente
    //          oportunidadPracticas.Organizacion = organizacionExistente;
    //          oportunidadPracticas.CoordinadorOrganizacion = coordinadorExistente;

    //          // Agregar la oportunidad de prácticas al contexto
    //          context.OportunidadPracticas.Add(oportunidadPracticas);

    //          // Guardar los cambios en la base de datos
    //          await context.SaveChangesAsync();
    //        }
    //      }
    //    }

    public static async Task SeedOportunidadPracticasAsync(
    UserManager<Users> userManager,
    RoleManager<IdentityRole> roleManager,
    ApplicationDbContext context
)
    {
      // Obtener las organizaciones existentes (puedes ajustar esto según tus necesidades)
      var organizaciones = await context.Organizaciones.ToListAsync();

      foreach (var organizacionExistente in organizaciones)
      {
        // Obtener el coordinador de la organización existente
        var coordinadorExistente = await context.CoordinadorOrganizacion
            .FirstOrDefaultAsync(c => c.CoordinadorOrganizacionId == organizacionExistente.CoordinadorOrganizacionId);

        if (coordinadorExistente != null)
        {
          for (int i = 0; i < 10; i++)
          {
            // Crear una nueva oportunidad de prácticas
            var oportunidadPracticas = new OportunidadesPracticas
            {
              OrganizacionId = organizacionExistente.OrganizacionId,
              CoordinadorOrganizacionId = coordinadorExistente.CoordinadorOrganizacionId,
              Descripcion = "Descripción de la oportunidad de prácticas",
              Requisitos = "Requisitos para la oportunidad de prácticas",
              FechaInicio = DateTime.Now,
              FechaFin = DateTime.Now.AddDays(30)
            };

            // Asociar la oportunidad de prácticas con la organización existente
            oportunidadPracticas.Organizacion = organizacionExistente;
            oportunidadPracticas.CoordinadorOrganizacion = coordinadorExistente;

            // Agregar la oportunidad de prácticas al contexto
            context.OportunidadPracticas.Add(oportunidadPracticas);
          }

          // Guardar los cambios en la base de datos
          await context.SaveChangesAsync();
        }
      }
    }

  }
}