using Microsoft.AspNetCore.Mvc.Rendering;
using  GlampingProyect.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;
using  GlampingProyect.Web.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace  GlampingProyect.Web.DTOs
{
    public class UserDTO
    {
        public string? Id { get; set; }

        [Display(Name = "Documento")]
        [MaxLength(32, ErrorMessage = "Elcampo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Document { get; set; } = null!;

        [Display(Name = "Nombres")]
        [MaxLength(32, ErrorMessage = "Elcampo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Apellidos")]
        [MaxLength(32, ErrorMessage = "Elcampo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string LastName { get; set; } = null!;

        [Display(Name = "Teléfono")]
        [MaxLength(32, ErrorMessage = "Elcampo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string PhoneNumber { get; set; } = null!;

        [MaxLength(64, ErrorMessage = "Elcampo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Email { get; set; } = null!;

        [Display(Name = "Rol")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un rol")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public int GlampingRoleId { get; set; }

        public GlampingRole? GlampingRole { get; set; }

        public IEnumerable<SelectListItem>? GlampingRoles { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
