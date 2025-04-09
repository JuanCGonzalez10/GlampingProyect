using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlampingProyect.Web.Data.Entities
{
    public class Glamping : IId
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Glamping")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        [MaxLength(64, ErrorMessage = "El campo '{0}' no debe tener más de 64 caracteres.")]
        public string Name { get; set; } = null!;

        [Column(TypeName = "VARCHAR(MAX)")]
        [Display(Name = "Contenido")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string Content { get; set; } = null!;

        [Display(Name = "¿Está publicado?")]
        public bool IsPublished { get; set; }

        [Display(Name = "Categoría")]
        public Category Category { get; set; } = null!;

        [Display(Name = "Categoría")]
        public int CategoryId { get; set; }
    }
}
