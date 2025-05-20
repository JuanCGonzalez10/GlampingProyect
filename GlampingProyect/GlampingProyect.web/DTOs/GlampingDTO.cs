using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GlampingProyect.Web.Data.Entities;

namespace GlampingProyect.Web.DTOs
{
    public class GlampingDTO
    {
        public int Id { get; set; }

        [Display(Name = "Glamping")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        [MaxLength(64, ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; } = null!;

        [Column(TypeName = "VARCHAR(MAX)")]
        [Display(Name = "Contenido")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string Content { get; set; } = null!;

        [Display(Name = "¿Está publicado?")]
        public bool IsPublished { get; set; }

        
        public Category? Category { get; set; }

        [Display(Name = "Categoría")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categoría")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem>? Categories { get; set; }
    }
}