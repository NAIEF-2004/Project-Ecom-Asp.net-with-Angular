using AutoMapper;
using Ecom_Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Threading.Tasks;

namespace Ecom_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugController : ControllerBase
    {
        private readonly IUnitOfWork work;
        private readonly IMapper mp;

        public BugController(IUnitOfWork work, IMapper mp)
        {
            this.work = work;
            this.mp = mp;
        }
        [HttpGet("Not-Found")]
        public async Task<IActionResult> GetNotFound()
        {
            var category = await work.CategoryRepostiry.GetByIdAsync(999999999);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpGet("Server-Error")]
        public async Task<IActionResult> GetServerError()
        {
            var category = await work.CategoryRepostiry.GetByIdAsync(99999999);
            category.Name = "";//cause server error
            return Ok(category);

        
        }
        [HttpGet("Bad-Requst/{id}")]
        public async Task<IActionResult> GetBadRequst(int id)
        {
            return Ok();
        }
        [HttpGet("Bad-Requst")]
        public async Task<IActionResult> GetBadRequst()
        {
            return BadRequest();
        }
    } 
}
