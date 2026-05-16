using AutoMapper;
using Ecom_Core.DTO;
using Ecom_Core.Entites.Prudact;
using Ecom_Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

        /// <summary>
        /// Test endpoint to trigger a 404 error
        /// </summary>
        [HttpGet("not-found")]
        public async Task<IActionResult> GetNotFound()
        {
            var category = await work.CategoryRepository.GetByIdAsync(999999999);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        /// <summary>
        /// Test endpoint to trigger a server error
        /// </summary>
        [HttpGet("server-error")]
        public async Task<IActionResult> GetServerError()
        {
            var category = await work.CategoryRepository.GetByIdAsync(99999999);
            category.Name = "";//cause server error
            return Ok(category);
        }

        /// <summary>
        /// Test endpoint with ID parameter
        /// </summary>
        [HttpGet("bad-request/{id}")]
        public async Task<IActionResult> GetBadRequest(int id)
        {
            return Ok();
        }

        /// <summary>
        /// Test endpoint for bad request
        /// </summary>
        [HttpGet("bad-request")]
        public async Task<IActionResult> GetBadRequestNoParam()
        {
            return BadRequest();
        }
    } 
}
