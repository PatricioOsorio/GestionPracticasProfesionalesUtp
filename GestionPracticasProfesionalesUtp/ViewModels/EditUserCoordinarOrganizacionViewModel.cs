using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionPracticasProfesionalesUtp.ViewModels
{
  public class EditUserCoordinarOrganizacionViewModel
  {
    public string UserId { get; set; }

    [Required(ErrorMessage = "El campo Email es requerido.")]
    [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido.")]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "El campo Nombre es requerido.")]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "El campo Apellido Paterno es requerido.")]
    [Display(Name = "Apellido Paterno")]
    public string ApellidoPaterno { get; set; }

    [Required(ErrorMessage = "El campo Apellido Materno es requerido.")]
    [Display(Name = "Apellido Materno")]
    public string ApellidoMaterno { get; set; }

    [Display(Name = "Contraseña")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "El campo Contraseña y Confirmar contraseña no son iguales.")]
    [Display(Name = "Confirmar contraseña")]
    public string ConfirmPassword { get; set; }

    [Display(Name = "Rol")]
    public string RoleId { get; set; }

    // Propiedad adicional para el ID de la Organización seleccionada (solo para SUPERADMIN)
    public string OrganizationId { get; set; }

    public IEnumerable<SelectListItem> Roles { get; set; }

    [Display(Name = "Área")]
    public string Area { get; set; }

    public EditUserCoordinarOrganizacionViewModel()
    {
      Roles = new List<SelectListItem>();
    }
  }
}
