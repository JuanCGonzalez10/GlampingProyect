using System.ComponentModel.DataAnnotations;
using  GlampingProyect.Web.DTOs;
using  GlampingProyect.Web.DTOs;


namespace  GlampingProyect.Web.DTOs
{
    public class ForgotPasswordDTO
    {
        [Required(ErrorMessage = "El correo es requerido")]
        [EmailAddress(ErrorMessage = "Correo no válido")]
        public string Email { get; set; } = null!;
    }
}
