using  GlampingProyect.Web.Core;
using  GlampingProyect.Web.DTOs;

namespace  GlampingProyect.Web.Services
{
    public interface IEmailService
    {
        public Task<Response<object>> SendAsync(SendEmailDTO dto);
        public Task<Response<object>> SendResetPasswordEmailAsync(string email, string message, string resetTokenLink);
    }
}
