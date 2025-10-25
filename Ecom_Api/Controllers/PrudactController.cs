using AutoMapper;
using Ecom_Api.Helper;
using Ecom_Core.DTO;
using Ecom_Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

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
        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult> getbyidprudact (int id)
        {
            try
            {
                var prudact = await work.PrudactRepostiry.GetByIdAsync(id, x => x.category, x => x.photos);
                var result = mapper.Map<PrudactDTO>(prudact);
                if (prudact is null)
                {
                    return BadRequest(new ResponseAPI(400));
                }
                return Ok(result);

            }
            catch (Exception ex )
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost("add-Prudact")]
        [RequestSizeLimit(100_000_000)]// 100 MB لزيادة حجم الطلب المسموح به
        public async Task<ActionResult> Addprudact(AddprudactDTO prudactDTO) 
        {
            try
            {
                await work.PrudactRepostiry.AddAsync(prudactDTO);
              return Ok(new ResponseAPI(200, "Prudact added successfully"));

            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
        [HttpPut("update-prudact")]
        public async Task<ActionResult> UpdatePrudact(UpdateprudactDTO updateprudactDTO) 
        {
            try
            {
               await work.PrudactRepostiry.UpdateAsync(updateprudactDTO);
                return Ok( new ResponseAPI(200,"Prudact updated successfully"));
            }
            catch (Exception ex )
            {

                return BadRequest(new ResponseAPI(400,ex.Message));
            }
        }
    }
}
