using System.ComponentModel.DataAnnotations;

namespace  GlampingProyect.Web.DTOs
{
    public class RegisterDTO
    {
        [Required]
        [Display(Name = "Documento")]
        public string Document { get; set; } = null!;

        [Required]
        [Display(Name = "Nombres")]
        public string FirstName { get; set; } = null!;

        [Required]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [Display(Name = "Correo")]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; } = null!;

        [Required]
        [Display(Name = "Rol")]
        public int GlampingRoleId { get; set; }
    }
}
