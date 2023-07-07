using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GestionPracticasProfesionalesUtp.Models;

namespace GestionPracticasProfesionalesUtp.Models
{
  public class CoordinadorOrganizacion
  {
    [Key]
    [ForeignKey(nameof(Users))]
    public string CoordinadorOrganizacionId { get; set; }

    [Column(TypeName = "nvarchar(150)")]
    [Display(Name = "Área")]
    public string Area { get; set; }

    // Propiedad de navegación para establecer la relación con Users
    public Users User { get; set; }

    // Propiedad de navegación inversa para establecer la relación uno a muchos con Organizaciones
    public ICollection<Organizaciones> Organizaciones { get; set; }

    // Propiedad de navegación inversa para establecer la relación uno a muchos con OportunidadPracticas
    public ICollection<OportunidadesPracticas> OportunidadPracticas { get; set; }
  }
}
