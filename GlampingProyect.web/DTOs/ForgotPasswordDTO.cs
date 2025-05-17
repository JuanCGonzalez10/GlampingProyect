using System.ComponentModel.DataAnnotations;
using GlampingProyect.Web.DTOs;
using GlampingProyect.web.DTOs;


namespace GlampingProyect.web.DTOs
{
    public class ForgotPasswordDTO
    {
        [Required(ErrorMessage = "El correo es requerido")]
        [EmailAddress(ErrorMessage = "Correo no válido")]
        public string Email { get; set; } = null!;
    }
}
