using System.ComponentModel.DataAnnotations;

namespace GlampingProyect.web.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [EmailAddress(ErrorMessage = "El campo {0} debe ser un Email válido.")]

        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MinLength(4, ErrorMessage = "El campo {0} debe tener por lo menos {1} caracteres.")]
        [Display(Name = "Contraseña")]
        public required string Password { get; set; }
    }
}
