using AutoMapper;
using Ecom_Api.Helper;
using Ecom_Core.DTO;
using Ecom_Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrudactController : ControllerBase
    {
        private readonly IUnitOfWork work;
        private readonly IMapper mapper;

        public PrudactController(IUnitOfWork work,IMapper mapper)
        {
            this.work = work;
            this.mapper = mapper;
        }
        [HttpGet("get-all-prodact")]
        public async Task<ActionResult> getallprudact() 
        {
            try
            {
                var prudact = await work.PrudactRepostiry.GetAllAsync(x=>x.category,x=>x.photos);
                var result = mapper.Map<List <PrudactDTO>>(prudact);
                if(prudact is null)
                {
                    return BadRequest(new ResponseAPI(400));
                }
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
