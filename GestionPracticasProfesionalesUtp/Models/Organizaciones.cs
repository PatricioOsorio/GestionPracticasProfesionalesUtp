using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GestionPracticasProfesionalesUtp.Models
{
  public class Organizaciones
  {
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "Matrícula")]
    public string OrganizacionId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    [Display(Name = "Nombre empresa")]
    public string Nombre { get; set; }

    [Column(TypeName = "nvarchar(150)")]
    [Display(Name = "Descripcion empresa")]
    public string Descripcion { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    [Display(Name = "Direccion empresa")]
    public string Direccion { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    [Display(Name = "Correo electronico")]
    public string Correo { get; set; }

    [Column(TypeName = "nvarchar(15)")]
    [Display(Name = "Telefono")]
    public string Telefono { get; set; }
  }
}
