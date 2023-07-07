using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionPracticasProfesionalesUtp.Models
{
  public class OportunidadesPracticas
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OportunidadPracticaId { get; set; }

    [Required]
    [ForeignKey(nameof(Organizacion))]
    public string OrganizacionId { get; set; }

    [Required]
    [ForeignKey(nameof(CoordinadorOrganizacion))]
    public string CoordinadorOrganizacionId { get; set; }

    [Required]
    [StringLength(1000)]
    public string Descripcion { get; set; }

    [Required]
    [StringLength(1000)]
    public string Requisitos { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime FechaInicio { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime FechaFin { get; set; }

    // Propiedad de navegación inversa para establecer la relación muchos a uno con Organizaciones
    public Organizaciones Organizacion { get; set; }

    // Propiedad de navegación inversa para establecer la relación muchos a uno con CoordinadorOrganizacion
    public CoordinadorOrganizacion CoordinadorOrganizacion { get; set; }
  }
}
