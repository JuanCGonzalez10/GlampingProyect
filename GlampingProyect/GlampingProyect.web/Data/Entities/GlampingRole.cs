using System.ComponentModel.DataAnnotations;

namespace GlampingProyect.web.Data.Entities
{
    public class GlampingRole
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Rol")]
        [MaxLength(32, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; } = null!;

        // Relaciones
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
        public ICollection<RoleCategory> RoleCategories { get; set; } = new List<RoleCategory>();
    }
}
