using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ecom_Api.MiddleWare
{

    //ان الفائدة من المدل وير هو عبارة عن بوابة لا يدخل الطلب مباشر للكنترولر وانما يمر عليها اولا
    public class ExceptionsMiddleWare
    {
        private readonly RequestDelegate next;

        public ExceptionsMiddleWare(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var response = new ApiExceptions((int)HttpStatusCode.InternalServerError,ex.Message,ex.StackTrace);
                var json=JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
