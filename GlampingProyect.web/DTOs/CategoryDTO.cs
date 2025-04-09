using System.ComponentModel.DataAnnotations;

namespace GlampingProyect.Web.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Display(Name = "Categorias")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Descripción")]
        public string? Description { get; set; }

        [Display(Name = "¿Está oculta?")]
        public bool IsHidden { get; set; }
    }
}
