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
        Nombre = "Patricio Miguel",
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
        UserName = "UTP9999999@hotmail.com",
        Email = "UTP9999999@hotmail.com",
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
            Matricula = "utp9999999".ToUpper(),
            UserId = newUser.Id,
            Carrera = "TI",
            Semestre = "9"
          };

          context.Students.Add(studentUser);
          await context.SaveChangesAsync();
        }

      }
    }

    public static async Task SeedUserOrganizacionesAsync(
      UserManager<Users> userManager,
      RoleManager<IdentityRole> roleManager,
      ApplicationDbContext context
      )
    {
      //Seed Organizacion - Jabil circuit
      var newOrganizacionUserJabil = new Users()
      {
        Nombre = "Jabil Circuit México",
        ApellidoPaterno = "NA",
        ApellidoMaterno = "NA",
        UserName = "jabil@hotmail.com",
        Email = "jabil@hotmail.com",
        EmailConfirmed = true,
        PhoneNumber = "2222222222"
      };
      if (userManager.Users.All(u => u.Id != newOrganizacionUserJabil.Id))
      {
        var user = await userManager.FindByEmailAsync(newOrganizacionUserJabil.Email);
        if (user == null)
        {
          await userManager.CreateAsync(newOrganizacionUserJabil, "Pato123.");
          await userManager.AddToRoleAsync(newOrganizacionUserJabil, Roles.ORGANIZACION.ToString());

          var userOrganizacion = new Organizaciones()
          {
            OrganizacionId = newOrganizacionUserJabil.Id,
            NombreOrganizacion = newOrganizacionUserJabil.Nombre,
            Descripcion = "Compañía de manufactura y servicios electrónicos para diversas industrias.",
            Direccion = "Autopista México-Puebla Km 116.6, Santa Clara, Cuautlancingo, Puebla.",
          };

          context.Organizaciones.Add(userOrganizacion);
          await context.SaveChangesAsync();
        }
      }

      //Seed Organizacion - La costeña
      var newOrganizacionUserCostena = new Users()
      {
        Nombre = "La Costeña",
        ApellidoPaterno = "NA",
        ApellidoMaterno = "NA",
        UserName = "la_costena@hotmail.com",
        Email = "la_costena@hotmail.com",
        EmailConfirmed = true,
        PhoneNumber = "2222222222"
      };
      if (userManager.Users.All(u => u.Id != newOrganizacionUserCostena.Id))
      {
        var user = await userManager.FindByEmailAsync(newOrganizacionUserCostena.Email);
        if (user == null)
        {
          await userManager.CreateAsync(newOrganizacionUserCostena, "Pato123.");
          await userManager.AddToRoleAsync(newOrganizacionUserCostena, Roles.ORGANIZACION.ToString());

          var userOrganizacion = new Organizaciones()
          {
            OrganizacionId = newOrganizacionUserCostena.Id,
            NombreOrganizacion = newOrganizacionUserCostena.Nombre,
            Descripcion = "Empresa dedicada a la producción y comercialización de alimentos enlatados.\r\n",
            Direccion = "Calle 15 de Mayo No. 1906, Colonia Centro, Puebla.",
          };

          context.Organizaciones.Add(userOrganizacion);
          await context.SaveChangesAsync();
        }
      }

      //Seed Organizacion - Grupo Bimbo
      var newOrganizacionUserBimbo = new Users()
      {
        Nombre = "Grupo Bimbo",
        ApellidoPaterno = "NA",
        ApellidoMaterno = "NA",
        UserName = "grupo_bimbo@hotmail.com",
        Email = "grupo_bimbo@hotmail.com",
        EmailConfirmed = true,
        PhoneNumber = "2222222222"
      };
      if (userManager.Users.All(u => u.Id != newOrganizacionUserBimbo.Id))
      {
        var user = await userManager.FindByEmailAsync(newOrganizacionUserBimbo.Email);
        if (user == null)
        {
          await userManager.CreateAsync(newOrganizacionUserBimbo, "Pato123.");
          await userManager.AddToRoleAsync(newOrganizacionUserBimbo, Roles.ORGANIZACION.ToString());

          var userOrganizacion = new Organizaciones()
          {
            OrganizacionId = newOrganizacionUserBimbo.Id,
            NombreOrganizacion = newOrganizacionUserBimbo.Nombre,
            Descripcion = "Empresa de panificación y productos alimenticios con presencia internacional",
            Direccion = "Avenida Cúmulo de Virgo No. 200, Parque Industrial Finsa, Puebla.",
          };

          context.Organizaciones.Add(userOrganizacion);
          await context.SaveChangesAsync();
        }
      }

      //Seed Organizacion - Grupo Modelo
      var newOrganizacionUserModelo = new Users()
      {
        Nombre = "Grupo Modelo",
        ApellidoPaterno = "NA",
        ApellidoMaterno = "NA",
        UserName = "grupo_modelo@hotmail.com",
        Email = "grupo_modelo@hotmail.com",
        EmailConfirmed = true,
        PhoneNumber = "2222222222"
      };
      if (userManager.Users.All(u => u.Id != newOrganizacionUserModelo.Id))
      {
        var user = await userManager.FindByEmailAsync(newOrganizacionUserModelo.Email);
        if (user == null)
        {
          await userManager.CreateAsync(newOrganizacionUserModelo, "Pato123.");
          await userManager.AddToRoleAsync(newOrganizacionUserModelo, Roles.ORGANIZACION.ToString());

          var userOrganizacion = new Organizaciones()
          {
            OrganizacionId = newOrganizacionUserModelo.Id,
            NombreOrganizacion = newOrganizacionUserModelo.Nombre,
            Descripcion = "Productor de cerveza reconocido a nivel mundial, con marcas como Corona, Modelo y Victoria.",
            Direccion = "Calle San Francisco Ocotlán No. 111, Puebla.",
          };

          context.Organizaciones.Add(userOrganizacion);
          await context.SaveChangesAsync();
        }
      }

      //Seed Organizacion - Audi México
      var newOrganizacionUserAudi = new Users()
      {
        Nombre = "Audi México",
        ApellidoPaterno = "NA",
        ApellidoMaterno = "NA",
        UserName = "audi_mexico@hotmail.com",
        Email = "audi_mexico@hotmail.com",
        EmailConfirmed = true,
        PhoneNumber = "2222222222"
      };
      if (userManager.Users.All(u => u.Id != newOrganizacionUserAudi.Id))
      {
        var user = await userManager.FindByEmailAsync(newOrganizacionUserAudi.Email);
        if (user == null)
        {
          await userManager.CreateAsync(newOrganizacionUserAudi, "Pato123.");
          await userManager.AddToRoleAsync(newOrganizacionUserAudi, Roles.ORGANIZACION.ToString());

          var userOrganizacion = new Organizaciones()
          {
            OrganizacionId = newOrganizacionUserAudi.Id,
            NombreOrganizacion = newOrganizacionUserAudi.Nombre,
            Descripcion = "Planta de producción de automóviles de lujo de la marca Audi.",
            Direccion = "San José Chiapa, Puebla.",
          };

          context.Organizaciones.Add(userOrganizacion);
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
        ApellidoPaterno = "Díaz",
        ApellidoMaterno = "Benitez",
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
      // Seed CoordinadorOrganizacionJabil User
      var newUserCoordinadorOrganizacionJabil = new Users()
      {
        Nombre = "Omar",
        ApellidoPaterno = "Silva",
        ApellidoMaterno = "Muñoz",
        UserName = "coordinador_organizacion_jabil@hotmail.com",
        Email = "coordinador_organizacion_jabil@hotmail.com",
        EmailConfirmed = true,
      };
      if (userManager.Users.All(u => u.Id != newUserCoordinadorOrganizacionJabil.Id))
      {
        var user = await userManager.FindByEmailAsync(newUserCoordinadorOrganizacionJabil.Email);
        if (user == null)
        {
          await userManager.CreateAsync(newUserCoordinadorOrganizacionJabil, "Pato123.");
          await userManager.AddToRoleAsync(newUserCoordinadorOrganizacionJabil, Roles.COORDINADOR_PRACTICA_ORGANIZACION.ToString());

          // Create CoordinadorOrganizacion record
          var coordinadorUser = new CoordinadorOrganizacion()
          {
            CoordinadorOrganizacionId = newUserCoordinadorOrganizacionJabil.Id,
            Area = "Desarrollo de software jabil 1",
          };

          context.CoordinadorOrganizacion.Add(coordinadorUser);
          await context.SaveChangesAsync();

          // Find the organization "Jabil"
          var organizacion = await context.Organizaciones.FirstOrDefaultAsync(o => o.NombreOrganizacion == "Jabil Circuit México");

          if (organizacion != null)
          {
            // Assign the CoordinadorOrganizacion to the organization
            coordinadorUser.OrganizacionId = organizacion.OrganizacionId;
            await context.SaveChangesAsync();
          }
        }
      }

      // Seed CoordinadorOrganizacionJabil2 User
      var newUserCoordinadorOrganizacionJabil2 = new Users()
      {
        Nombre = "Rosa",
        ApellidoPaterno = "Silva",
        ApellidoMaterno = "Cruz",
        UserName = "coordinador_organizacion_jabil2@hotmail.com",
        Email = "coordinador_organizacion_jabil2@hotmail.com",
        EmailConfirmed = true,
      };
      if (userManager.Users.All(u => u.Id != newUserCoordinadorOrganizacionJabil2.Id))
      {
        var user = await userManager.FindByEmailAsync(newUserCoordinadorOrganizacionJabil2.Email);
        if (user == null)
        {
          await userManager.CreateAsync(newUserCoordinadorOrganizacionJabil2, "Pato123.");
          await userManager.AddToRoleAsync(newUserCoordinadorOrganizacionJabil2, Roles.COORDINADOR_PRACTICA_ORGANIZACION.ToString());

          // Create CoordinadorOrganizacion record
          var coordinadorUser = new CoordinadorOrganizacion()
          {
            CoordinadorOrganizacionId = newUserCoordinadorOrganizacionJabil2.Id,
            Area = "Desarrollo de software jabil 2",
          };

          context.CoordinadorOrganizacion.Add(coordinadorUser);
          await context.SaveChangesAsync();

          // Find the organization "Jabil"
          var organizacion = await context.Organizaciones.FirstOrDefaultAsync(o => o.NombreOrganizacion == "Jabil Circuit México");

          if (organizacion != null)
          {
            // Assign the CoordinadorOrganizacion to the organization
            coordinadorUser.OrganizacionId = organizacion.OrganizacionId;
            await context.SaveChangesAsync();
          }
        }
      }

      // Seed CoordinadorOrganizacionLaCostena User
      var newUserCoordinadorOrganizacionLaCostena = new Users()
      {
        Nombre = "Karen",
        ApellidoPaterno = "Morillo",
        ApellidoMaterno = "Martinez",
        UserName = "coordinador_organizacion_la_costena@hotmail.com",
        Email = "coordinador_organizacion_la_costena@hotmail.com",
        EmailConfirmed = true,
      };
      if (userManager.Users.All(u => u.Id != newUserCoordinadorOrganizacionLaCostena.Id))
      {
        var user = await userManager.FindByEmailAsync(newUserCoordinadorOrganizacionLaCostena.Email);
        if (user == null)
        {
          await userManager.CreateAsync(newUserCoordinadorOrganizacionLaCostena, "Pato123.");
          await userManager.AddToRoleAsync(newUserCoordinadorOrganizacionLaCostena, Roles.COORDINADOR_PRACTICA_ORGANIZACION.ToString());

          // Create CoordinadorOrganizacion record
          var coordinadorUser = new CoordinadorOrganizacion()
          {
            CoordinadorOrganizacionId = newUserCoordinadorOrganizacionLaCostena.Id,
            Area = "Desarrollo de software la costeña",
          };

          context.CoordinadorOrganizacion.Add(coordinadorUser);
          await context.SaveChangesAsync();

          // Find the organization "La Costeña"
          var organizacion = await context.Organizaciones.FirstOrDefaultAsync(o => o.NombreOrganizacion == "La Costeña");

          if (organizacion != null)
          {
            // Assign the CoordinadorOrganizacion to the organization
            coordinadorUser.OrganizacionId = organizacion.OrganizacionId;
            await context.SaveChangesAsync();
          }
        }
      }

      // Seed CoordinadorOrganizacionBimbo User
      var newUserCoordinadorOrganizacionBimbo = new Users()
      {
        Nombre = "René",
        ApellidoPaterno = "Sosa",
        ApellidoMaterno = "Britez",
        UserName = "coordinador_organizacion_bimbo@hotmail.com",
        Email = "coordinador_organizacion_bimbo@hotmail.com",
        EmailConfirmed = true,
      };
      if (userManager.Users.All(u => u.Id != newUserCoordinadorOrganizacionBimbo.Id))
      {
        var user = await userManager.FindByEmailAsync(newUserCoordinadorOrganizacionBimbo.Email);
        if (user == null)
        {
          await userManager.CreateAsync(newUserCoordinadorOrganizacionBimbo, "Pato123.");
          await userManager.AddToRoleAsync(newUserCoordinadorOrganizacionBimbo, Roles.COORDINADOR_PRACTICA_ORGANIZACION.ToString());

          // Create CoordinadorOrganizacion record
          var coordinadorUser = new CoordinadorOrganizacion()
          {
            CoordinadorOrganizacionId = newUserCoordinadorOrganizacionBimbo.Id,
            Area = "Desarrollo de software - Bimbo",
          };

          context.CoordinadorOrganizacion.Add(coordinadorUser);
          await context.SaveChangesAsync();

          // Find the organization "Grupo Bimbo"
          var organizacion = await context.Organizaciones.FirstOrDefaultAsync(o => o.NombreOrganizacion == "Grupo Bimbo");

          if (organizacion != null)
          {
            // Assign the CoordinadorOrganizacion to the organization
            coordinadorUser.OrganizacionId = organizacion.OrganizacionId;
            await context.SaveChangesAsync();
          }
        }
      }

      // Seed CoordinadorOrganizacionModelo User
      var newUserCoordinadorOrganizacionModelo = new Users()
      {
        Nombre = "Juan Nicolas",
        ApellidoPaterno = "Romero",
        ApellidoMaterno = "Torres",
        UserName = "coordinador_organizacion_modelo@hotmail.com",
        Email = "coordinador_organizacion_modelo@hotmail.com",
        EmailConfirmed = true,
      };
      if (userManager.Users.All(u => u.Id != newUserCoordinadorOrganizacionModelo.Id))
      {
        var user = await userManager.FindByEmailAsync(newUserCoordinadorOrganizacionModelo.Email);
        if (user == null)
        {
          await userManager.CreateAsync(newUserCoordinadorOrganizacionModelo, "Pato123.");
          await userManager.AddToRoleAsync(newUserCoordinadorOrganizacionModelo, Roles.COORDINADOR_PRACTICA_ORGANIZACION.ToString());

          // Create CoordinadorOrganizacion record
          var coordinadorUser = new CoordinadorOrganizacion()
          {
            CoordinadorOrganizacionId = newUserCoordinadorOrganizacionModelo.Id,
            Area = "Desarrollo de software - Modelo",
          };

          context.CoordinadorOrganizacion.Add(coordinadorUser);
          await context.SaveChangesAsync();

          // Find the organization "Grupo Modelo"
          var organizacion = await context.Organizaciones.FirstOrDefaultAsync(o => o.NombreOrganizacion == "Grupo Modelo");

          if (organizacion != null)
          {
            // Assign the CoordinadorOrganizacion to the organization
            coordinadorUser.OrganizacionId = organizacion.OrganizacionId;
            await context.SaveChangesAsync();
          }
        }
      }

      // Seed CoordinadorOrganizacionAudi User
      var newUserCoordinadorOrganizacionAudi = new Users()
      {
        Nombre = "Ana Vega",
        ApellidoPaterno = "Caballero",
        ApellidoMaterno = "Caballero",
        UserName = "coordinador_organizacion_audi@hotmail.com",
        Email = "coordinador_organizacion_audi@hotmail.com",
        EmailConfirmed = true,
      };
      if (userManager.Users.All(u => u.Id != newUserCoordinadorOrganizacionAudi.Id))
      {
        var user = await userManager.FindByEmailAsync(newUserCoordinadorOrganizacionAudi.Email);
        if (user == null)
        {
          await userManager.CreateAsync(newUserCoordinadorOrganizacionAudi, "Pato123.");
          await userManager.AddToRoleAsync(newUserCoordinadorOrganizacionAudi, Roles.COORDINADOR_PRACTICA_ORGANIZACION.ToString());

          // Create CoordinadorOrganizacion record
          var coordinadorUser = new CoordinadorOrganizacion()
          {
            CoordinadorOrganizacionId = newUserCoordinadorOrganizacionAudi.Id,
            Area = "Desarrollo de software - Audi",
          };

          context.CoordinadorOrganizacion.Add(coordinadorUser);
          await context.SaveChangesAsync();

          // Find the organization "Audi México"
          var organizacion = await context.Organizaciones.FirstOrDefaultAsync(o => o.NombreOrganizacion == "Audi México");

          if (organizacion != null)
          {
            // Assign the CoordinadorOrganizacion to the organization
            coordinadorUser.OrganizacionId = organizacion.OrganizacionId;
            await context.SaveChangesAsync();
          }
        }
      }
    }

    //public static async Task SeedOportunidadPracticasAsync(
    //  UserManager<Users> userManager,
    //  RoleManager<IdentityRole> roleManager,
    //  ApplicationDbContext context
    //)
    //{
    //  // Obtener las organizaciones existentes (puedes ajustar esto según tus necesidades)
    //  var organizaciones = await context.Organizaciones.ToListAsync();

    //  foreach (var organizacionExistente in organizaciones)
    //  {
    //    // Obtener el coordinador de la organización existente
    //    var coordinadorExistente = await context.CoordinadorOrganizacion
    //        .FirstOrDefaultAsync(c => c.OrganizacionId == organizacionExistente.OrganizacionId);

    //    if (coordinadorExistente != null)
    //    {
    //      for (int i = 0; i < 10; i++)
    //      {
    //        // Crear una nueva oportunidad de prácticas
    //        var oportunidadPracticas = new OportunidadesPracticas
    //        {
    //          OrganizacionId = organizacionExistente.OrganizacionId,
    //          CoordinadorOrganizacionId = coordinadorExistente.CoordinadorOrganizacionId,
    //          Descripcion = "Descripción de la oportunidad de prácticas",
    //          Requisitos = "Requisitos para la oportunidad de prácticas",
    //          FechaInicio = DateTime.Now,
    //          FechaFin = DateTime.Now.AddDays(30)
    //        };

    //        // Asociar la oportunidad de prácticas con la organización existente
    //        oportunidadPracticas.Organizacion = organizacionExistente;
    //        oportunidadPracticas.CoordinadorOrganizacion = coordinadorExistente;

    //        // Agregar la oportunidad de prácticas al contexto
    //        context.OportunidadPracticas.Add(oportunidadPracticas);
    //      }

    //      // Guardar los cambios en la base de datos
    //      await context.SaveChangesAsync();
    //    }
    //  }
    //}


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
            .FirstOrDefaultAsync(c => c.OrganizacionId == organizacionExistente.OrganizacionId);

        if (coordinadorExistente != null)
        {
          var oportunidadPracticas = new OportunidadesPracticas
          {
            OrganizacionId = organizacionExistente.OrganizacionId,
            CoordinadorOrganizacionId = coordinadorExistente.CoordinadorOrganizacionId,
            Descripcion = "Descripción de la oportunidad de prácticas ",
            Requisitos = "Requisitos para la oportunidad de prácticas ",
            FechaInicio = DateTime.Now,
            FechaFin = DateTime.Now.AddDays(30)
          };

          // Asociar la oportunidad de prácticas con la organización existente
          oportunidadPracticas.Organizacion = organizacionExistente;
          oportunidadPracticas.CoordinadorOrganizacion = coordinadorExistente;

          // Agregar la oportunidad de prácticas al contexto
          context.OportunidadPracticas.Add(oportunidadPracticas);

          // Guardar los cambios en la base de datos
          await context.SaveChangesAsync();
        }
      }
    }


  }
}