using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GestionPracticasProfesionalesUtp.Models
{
  public class Student
  {
    [Key]
    public int StudentId { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(50)")]
    [Display(Name = "Nombres")]
    public string Nombres { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(50)")]
    [Display(Name = "Apellido Paterno")]
    public string ApellidoPat { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(50)")]
    [Display(Name = "Apellido Materno")]
    public string ApellidoMat { get; set; }
  }
}
