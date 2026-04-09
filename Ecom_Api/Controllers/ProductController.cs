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
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork work;
        private readonly IMapper mapper;

        public ProductController(IUnitOfWork work,IMapper mapper)
        {
            this.work = work;
            this.mapper = mapper;
        }
        [HttpGet("get-all-product")]
        public async Task<ActionResult> getallproduct() 
        {
            try
            {
                var product = await work.ProductRepository.GetAllAsync(x=>x.category,x=>x.photos);
                var result = mapper.Map<List <ProductDTO>>(product);
                if(product is null)
                {
                    return NotFound(new ResponseAPI(404));
                }
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult> getbyidproduct (int id)
        {
            try
            {
                var product = await work.ProductRepository.GetByIdAsync(id, x => x.category, x => x.photos);
                var result = mapper.Map<ProductDTO>(product);
                if (product is null)
                {
                    return NotFound(new ResponseAPI(404));
                }
                return Ok(result);

            }
            catch (Exception ex )
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost("add-Product")]
        [RequestSizeLimit(100_000_000)]// 100 MB لزيادة حجم الطلب المسموح به
        public async Task<ActionResult> AddProduct(AddProductDTO productDTO) 
        {
            try
            {
                await work.ProductRepository.AddAsync(productDTO);
              return Ok(new ResponseAPI(200, "Product added successfully"));

            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
        [HttpPut("update-product")]
        public async Task<ActionResult> UpdateProduct(UpdateProductDTO updateProductDTO) 
        {
            try
            {
               await work.ProductRepository.UpdateAsync(updateProductDTO);
                return Ok( new ResponseAPI(200,"Product updated successfully"));
            }
            catch (Exception ex )
            {

                return BadRequest(new ResponseAPI(400,ex.Message));
            }
        }
        [HttpDelete("Delete-Product/{id}")]
        public async Task<IActionResult> DeleteProduct(int id) 
        {
            try
            {
                var product = await work.ProductRepository.GetByIdAsync(id, x => x.photos, x => x.category);
                await work.ProductRepository.DeleteAsync(product);
                return Ok(new ResponseAPI(200, "Product deleted successfully"));
            }
            catch (Exception ex)
            {

               return BadRequest(ex.Message);
            }
 

        }
    }
}
