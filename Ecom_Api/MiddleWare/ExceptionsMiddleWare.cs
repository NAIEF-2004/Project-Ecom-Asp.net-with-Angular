using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ecom_Api.MiddleWare
{

    //ان الفائدة من المدل وير هو عبارة عن بوابة لا يدخل الطلب مباشر للكنترولر وانما يمر عليها اولا
    public class ExceptionsMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly IHostEnvironment environment;

        public ExceptionsMiddleWare(RequestDelegate next,IHostEnvironment Environment)
        {
            this.next = next;
            environment = Environment;
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
                //عملت هي الحركة مشان بوضع التطورير ممكن القراءة اما في ال isprodaction ما يحسن يقرء الخطء شخص غريب لكي لا يخترقققق
                var response = environment.IsDevelopment() ?
                    new ApiExceptions((int)HttpStatusCode.InternalServerError,ex.Message,ex.StackTrace)
                    : new ApiExceptions((int)HttpStatusCode.InternalServerError, ex.Message);

                var json=JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
