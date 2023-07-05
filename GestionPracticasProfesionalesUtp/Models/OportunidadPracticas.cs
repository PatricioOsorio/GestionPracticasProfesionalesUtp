using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GestionPracticasProfesionalesUtp.Models
{
  public class OportunidadPracticas
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string OportunidadPracticaId { get; set; }

    [ForeignKey(nameof(Organizaciones))]
    public string OrganizacionId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(500)")]
    [Display(Name = "Descripcion del puesto")]
    public string Descripcion { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(500)")]
    [Display(Name = "Requisitos del puesto")]
    public string Requisitos { get; set; }

    [Required]
    [Display(Name = "Fecha de Inicio")]
    [DataType(DataType.Date)]
    public DateTime FechaInicio { get; set; }

    [Required]
    [Display(Name = "Fecha de Fin")]
    [DataType(DataType.Date)]
    public DateTime FechaFin { get; set; }

    public Organizaciones Organizacion { get; set; }
  }
}
