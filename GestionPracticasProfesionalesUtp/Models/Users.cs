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
    public string? Nombre { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(50)")]
    [Display(Name = "Apellido paterno")]
    public string? ApellidoPaterno { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(50)")]
    [Display(Name = "Apellido materno")]
    public string? ApellidoMaterno { get; set; }
  }
}
