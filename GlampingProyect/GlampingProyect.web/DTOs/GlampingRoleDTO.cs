using  GlampingProyect.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace  GlampingProyect.Web.DTOs
{
    public class GlampingRoleDTO
    {
        public int Id { get; set; }

        [Display(Name = "Rol")]
        [MaxLength(64, ErrorMessage = "Elcampo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; } = null!;

        public List<PermissionForRoleDTO>? Permissions { get; set; }
        public string? PermissionIds { get; set; }

        public List<CategoryForRoleDTO>? Categories { get; set; }
        public string? CategoryIds { get; set; }
    }

    public class CategoryForRoleDTO : CategoryDTO
    {
        public bool Selected { get; set; }
    }

    public class PermissionForRoleDTO : PermissionDTO
    {
        public bool Selected { get; set; }
    }
}
