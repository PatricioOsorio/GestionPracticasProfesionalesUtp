using EllipticCurve.Utils;
using GestionPracticasProfesionalesUtp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GestionPracticasProfesionalesUtp.ViewModels
{
  public class UpdateUserViewModel
  {
    [Required]
    public string Id { get; set; }

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


    [Required(ErrorMessage = "El campo Rol es requerido.")]
    [Display(Name = "Rol")]
    public string RoleId { get; set; }

    public IEnumerable<SelectListItem> Roles { get; set; }

    // Propiedad adicional para almacenar el ID del rol seleccionado
    public string SelectedRoleId { get; set; }

    public UpdateUserViewModel()
    {
      Roles = new List<SelectListItem>();
      SelectedRoleId = string.Empty;
    }
  }
}
