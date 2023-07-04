using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionPracticasProfesionalesUtp.Models
{
  public class CoordinadorPracticas
  {
    [Key]
    [ForeignKey(nameof(User))]
    public string CoordinadorPracticaId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string Departamento { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string Facultad { get; set; }


    // Propiedad de navegación hacia el usuario
    public Users User { get; set; }
  }
}
