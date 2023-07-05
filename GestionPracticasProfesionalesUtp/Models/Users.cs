using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GestionPracticasProfesionalesUtp.Models
{
  public class Users : IdentityUser
  {
    [PersonalData]
    [Column(TypeName = "nvarchar(50)")]
    [Display(Name = "Nombre")]
    [Required]
    public string? Nombre { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(50)")]
    [Display(Name = "Apellido paterno")]
    [Required]
    public string? ApellidoPaterno { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(50)")]
    [Display(Name = "Apellido materno")]
    [Required]
    public string? ApellidoMaterno { get; set; }

    // Agregado: Propiedad de navegación inversa para establecer la relación uno a uno
    public Students Student { get; set; }

    // Agregado: Propiedad de navegación inversa para establecer la relación uno a uno
    public CoordinadorPracticas CoordinadorPractica { get; set; }
  }
}
