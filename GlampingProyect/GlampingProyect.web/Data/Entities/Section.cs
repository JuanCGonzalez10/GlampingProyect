using System.ComponentModel.DataAnnotations;

namespace GlampingProyect.web.Data.Entities
{
    public class Section
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Sección")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Descripción")]
        public string Description { get; set; } = null!;

        [Display(Name = "¿Está oculta?")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public bool isHidden { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; }
        public ICollection<RoleSection>? RoleSections { get; set; }
    }
}
