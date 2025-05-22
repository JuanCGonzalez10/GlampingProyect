using System.ComponentModel.DataAnnotations;

namespace  GlampingProyect.Web.Data.Entities
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Permiso")]
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Descripción")]
        [MaxLength(512, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Description { get; set; } = null!;

        [Display(Name = "Módulo")]
        [MaxLength(32, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Module { get; set; } = null!;

        // Relaciones
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
