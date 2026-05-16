using Ecom_Api.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom_Api.Controllers
{
    [Route("Error/{StatusCode}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// Handle API errors
        /// </summary>
        [HttpGet]
        public ActionResult Error(int statusCode) 
        {
            return new ObjectResult(new ResponseAPI(statusCode))
            {
                StatusCode = statusCode
            };
        }
    }
}
