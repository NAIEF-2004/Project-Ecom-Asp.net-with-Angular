using Ecom_Api.Helper;

namespace Ecom_Api.MiddleWare
{
    public class ApiExceptions : ResponseAPI
    {
        public ApiExceptions(int statusCod, string message = null, string Details=null) : base(statusCod, message)
        {
           this.Details = Details;
        }
        public string Details { get; set; }
    }
}
