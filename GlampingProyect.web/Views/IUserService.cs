using GlampingProyect.web.Data.Entities;
using GlampingProyect.web.DTOs;
using GlampingProyect.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace GlampingProyect.web.Services
{
    public interface IUserService
    {
        // Registro y confirmación
        Task<IdentityResult> AddUserAsync(User user, string password);
        Task<IdentityResult> ConfirmEmailAsync(User user, string token);
        Task<string> GenerateEmailConfirmationTokenAsync(User user);

        // Búsqueda de usuario
        Task<User?> FindByEmailAsync(string email);

        // Login / Logout
        Task<SignInResult> LoginAsync(LoginDTO dto);
        Task LogoutAsync();

        // Restablecimiento de contraseña
        Task<string> GeneratePasswordResetTokenAsync(User user);
        Task SendPasswordResetEmailAsync(string email, string resetLink);
        Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);
    }

    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;

        public UserService(
            DataContext context,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        // --------------------------------------------------
        // Registro y confirmación de email
        public async Task<IdentityResult> AddUserAsync(User user, string password)
            => await _userManager.CreateAsync(user, password);

        public async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
            => await _userManager.ConfirmEmailAsync(user, token);

        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
            => await _userManager.GenerateEmailConfirmationTokenAsync(user);

        // --------------------------------------------------
        // Búsqueda de usuario
        public async Task<User?> FindByEmailAsync(string email)
            => await _userManager.FindByEmailAsync(email);

        // --------------------------------------------------
        // Login / Logout
        public async Task<SignInResult> LoginAsync(LoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return SignInResult.Failed;

            return await _signInManager.PasswordSignInAsync(
                user.UserName,
                dto.Password,
                isPersistent: false,
                lockoutOnFailure: false);
        }

        public async Task LogoutAsync()
            => await _signInManager.SignOutAsync();

        // --------------------------------------------------
        // Restablecimiento de contraseña
        public async Task<string> GeneratePasswordResetTokenAsync(User user)
            => await _userManager.GeneratePasswordResetTokenAsync(user);

        public async Task SendPasswordResetEmailAsync(string email, string resetLink)
        {
            var subject = "Restablecer tu contraseña - Glamping";
            var body = $@"
                <p>Para restablecer tu contraseña, haz clic en el siguiente enlace:</p>
                <p><a href='{resetLink}'>Restablecer contraseña</a></p>";

            await _emailSender.SendEmailAsync(email, subject, body);
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
            => await _userManager.ResetPasswordAsync(user, token, newPassword);
    }
}
