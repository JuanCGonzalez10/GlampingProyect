using System.ComponentModel.DataAnnotations;
using GlampingProyect.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GlampingProyect.Web.DTOs
{
    public class ProductCategoryDTO : IId
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(32, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Categoria Del Producto")]
        public string ProductCategoryName { get; set; } = null!;
    }
}