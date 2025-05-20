using GlampingProyect.web.DTOs;
using GlampingProyect.Web.DTOs;
using GlampingProyect.web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace GlampingProyect.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        // === Login ===
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginAsync(dto);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");

                ModelState.AddModelError(string.Empty, "Email o contraseña incorrectos");
            }
            return View(dto);
        }

        // === Logout ===
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }

        // === Error handling ===
        [HttpGet]
        [Route("Errors/{statusCode:int}")]
        public IActionResult Error(int statusCode)
        {
            string errorMessage = statusCode switch
            {
                StatusCodes.Status401Unauthorized => "Debes iniciar sesión.",
                StatusCodes.Status403Forbidden => "No tienes permiso para estar aquí.",
                StatusCodes.Status404NotFound => "La página que estás intentando acceder no existe",
                _ => "Ha ocurrido un error"
            };
            ViewBag.ErrorMessage = errorMessage;
            return View(statusCode);
        }

        // === Access Denied ===
        [HttpGet]
        public IActionResult NoAuthorized()
        {
            return View();
        }

        // === Forgot Password ===
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userService.FindByEmailAsync(model.Email);
            // No revelar si el usuario existe
            if (user == null)
            {
                ViewBag.Message = "Si el correo está registrado, recibirás un enlace para restablecer tu contraseña.";
                return View();
            }

            var token = await _userService.GeneratePasswordResetTokenAsync(user);
            var resetLink = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme);

            await _userService.SendPasswordResetEmailAsync(user.Email, resetLink);

            ViewBag.Message = "Hemos enviado un enlace a tu correo para restablecer tu contraseña.";
            return View();
        }

        // === Reset Password ===
        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
                return RedirectToAction(nameof(Login));

            var model = new ResetPasswordDTO { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userService.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Error al restablecer la contraseña.");
                return View(model);
            }

            var result = await _userService.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (result.Succeeded)
                return RedirectToAction(nameof(Login), new { Message = "Contraseña restablecida con éxito." });

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }
    }
}
