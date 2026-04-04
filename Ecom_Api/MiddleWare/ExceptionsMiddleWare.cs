using Microsoft.Extensions.Caching.Memory;
using System.Data;
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
        private readonly IMemoryCache memoryCache;
        private readonly TimeSpan ratelimit = TimeSpan.FromSeconds(10);

        public ExceptionsMiddleWare(RequestDelegate next, IHostEnvironment Environment, IMemoryCache memoryCache)
        {
            this.next = next;
            environment = Environment;
            this.memoryCache = memoryCache;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                if (IsRequstAllowd(context) == false)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    context.Response.ContentType = "application/json";

                    var response =
                        new ApiExceptions((int)HttpStatusCode.TooManyRequests, "Too Many Response, please try again later");

                    await context.Response.WriteAsJsonAsync(response);

                    return;
                }
                await next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                //عملت هي الحركة مشان بوضع التطورير ممكن القراءة اما في ال isprodaction ما يحسن يقرء الخطء شخص غريب لكي لا يخترقققق
                var response = environment.IsDevelopment() ?
                    new ApiExceptions((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                    : new ApiExceptions((int)HttpStatusCode.InternalServerError, ex.Message);

                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }
        //في حال مستخدم عمل بوت تكرار الارسال ليضربلك الفكرة فهي الدالة تقف في وجهه
        //ratelimet
        private bool IsRequstAllowd(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var cachKey = $"Rate:{ip}";
            var dateNow = DateTime.Now;

            var (timesTamp, count) = memoryCache.GetOrCreate( cachKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = ratelimit;
                return (timesTamp: dateNow, count: 0);
            });

            if (dateNow - timesTamp < ratelimit)
            {
                if (count >= 8)
                {
                    return false;
                }

                memoryCache.Set( cachKey, (timesTamp, count += 1), ratelimit);
            }
            else
            {
                memoryCache.Set(cachKey,  (timesTamp, count),ratelimit);
            }

            return true;
        }
    }
}
