using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GestionPracticasProfesionalesUtp.Models
{
  public class Students
  {
    [Key]
    [Required]
    [Column(TypeName = "nvarchar(10)")]
    [Display(Name = "Matrícula")]
    public string Matricula { get; set; }

    [Required]
    [ForeignKey(nameof(User))]
    public string UserId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    [Display(Name = "Carrera")]
    public string Carrera { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    [Display(Name = "Semestre")]
    public string Semestre { get; set; }

    // Propiedad de navegación hacia el usuario
    public Users User { get; set; }
  }
}
