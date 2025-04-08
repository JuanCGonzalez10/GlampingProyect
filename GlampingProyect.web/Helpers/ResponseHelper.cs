using GlampingProyect.Web.Core;
using Library1.Cor;


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
            return new Res<T> 
            {
                IsSuccess = false,
                Message = message,
                Errors = new List<string>
                {
                    ex.Message,
                    //ex.InnerException.Message
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
