using Ecom_Api.Helper;

namespace Ecom_Api.MiddleWare
{
    public class ApiExceptions : ResponseAPI
    {
        public ApiExceptions(int statusCod, string? message = null, string? details = null) : base(statusCod, message)
        {
            Details = details;
        }

        public string? Details { get; set; }
    }
}
