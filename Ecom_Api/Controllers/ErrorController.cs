using Ecom_Api.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom_Api.Controllers
{
    [Route("Error/{StatusCod}")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        //في حال طلب اند بوينت غير موجودة ياخذني لهنا ويرجعلي رسالة الخطء المناسبة
        [HttpGet]
        public ActionResult Error(int StatusCod) 
        {
            return new ObjectResult(new ResponseAPI(StatusCod))
            {
                StatusCode = StatusCod
            };
        }
    }
}
