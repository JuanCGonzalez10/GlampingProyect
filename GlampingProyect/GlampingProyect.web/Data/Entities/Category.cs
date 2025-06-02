
using System.ComponentModel.DataAnnotations;
using  GlampingProyect.Web.Data.Entities;


namespace  GlampingProyect.Web.Data.Entities
{
    public class Category : IId
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Descripción")]
        public string? Description { get; set; }

        [Display(Name = "¿Está oculta?")]
        public bool IsHidden { get; set; }

        public ICollection<RoleCategory>? RoleCategories { get; set; }
    }
}
