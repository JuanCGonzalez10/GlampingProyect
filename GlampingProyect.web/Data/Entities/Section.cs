using System.ComponentModel.DataAnnotations;

namespace GlampingProyect.web.Data.Entities
{
    public class Section
    {
        [key]
        public int Id { get; set; }

        [Display(Name = "seccion")]
        [Required(ErrorMessage = "el campo '{0}' es requerido")]
        public string Name { get; set; } = null!;

        [Display(Name = "Descripcion")]

        public string? Descripcion { get; set; }

        [Display(Name = "Esta oculta?")]
        public bool IsHidden { get; set; }


    }
}
