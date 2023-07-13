using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GestionPracticasProfesionalesUtp.Models
{
  public class Organizaciones
  {
    [Key]
    [ForeignKey(nameof(User))]
    public string OrganizacionId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(200)")]
    [Display(Name = "Nombre empresa")]
    public string NombreOrganizacion { get; set; }

    [Column(TypeName = "nvarchar(500)")]
    [Display(Name = "Descripcion empresa")]
    public string Descripcion { get; set; }


    [Column(TypeName = "nvarchar(500)")]
    [Display(Name = "Descripcion empresa")]
    public string Direccion { get; set; }

    // Propiedad de navegación inversa para establecer la relación uno a muchos con OportunidadPracticas
    public ICollection<OportunidadesPracticas> OportunidadPracticas { get; set; }


    // Cambia a ICollection<CoordinadorOrganizacion>
    public ICollection<CoordinadorOrganizacion> CoordinadorOrganizacion { get; set; }

    // Propiedad de navegación inversa para establecer la relación con Users
    [ForeignKey(nameof(OrganizacionId))]
    public Users? User { get; set; }

  }
}
