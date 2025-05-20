using GlampingProyect.Web.Core;
using Library1.Cor;
using Microsoft.EntityFrameworkCore;


namespace GlampingProyect.Web.Helpers
{
    public static class ResponseHelper<T>
    {
        public static Res<T> MakeResponseSuccess(T model, string message = "tarea realizada con éxito")
        {
            return new Res<T>
            {
                IsSuccess = true,
                Message = message,
                MyProperty = model,
            };
        }
        public static Res<T> MakeResponseSuccess(string message = "Tarea realizada con éxito")
        {
            return new Res<T>
            {
                IsSuccess = true,
                Message = message,
            };
        }

        public static Res<T> MakeResponseFail(Exception ex, string message = "Ha ocurrido un error al generar la solicitud.")
        {
            string detailedMessage;

            // Si es una excepción de base de datos, intenta obtener más detalle
            if (ex is DbUpdateException dbEx)
            {
                detailedMessage = $"Error al guardar en la base de datos: {dbEx.InnerException?.Message ?? dbEx.Message}";
            }
            else
            {
                detailedMessage = ex.Message;
            }

            return new Res<T>
            {
                IsSuccess = false,
                Message = $"{message}: {detailedMessage}",
                Errors = new List<string>
        {
            detailedMessage
        }
            };
        }

        public static Res<T> MakeResponseFail(string message = "Ha ocurrido un error al generar la solicitud.")
        {
            return new Res<T>
            {
                IsSuccess = false,
                Message = message,
            };
        }
    }
}
