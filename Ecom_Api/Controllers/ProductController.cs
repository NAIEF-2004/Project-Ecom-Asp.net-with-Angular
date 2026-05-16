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

        public ProductController(IUnitOfWork work, IMapper mapper)
        {
            this.work = work;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        [HttpGet("get-all-product")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAllProduct() 
        {
            try
            {
                var products = await work.ProductRepository.GetAllAsync(x => x.category, x => x.photos);
                var result = mapper.Map<List<ProductDTO>>(products);
                if (products is null || !products.Any())
                {
                    return NotFound(new ResponseAPI(404, "No products found"));
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }

        /// <summary>
        /// Get product by ID
        /// </summary>
        [HttpGet("get-by-id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetByIdProduct(int id)
        {
            try
            {
                var product = await work.ProductRepository.GetByIdAsync(id, x => x.category, x => x.photos);
                var result = mapper.Map<ProductDTO>(product);
                if (product is null)
                {
                    return NotFound(new ResponseAPI(404, "Product not found"));
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }

        /// <summary>
        /// Add new product
        /// </summary>
        [HttpPost("add-product")]
        [RequestSizeLimit(100_000_000)]// 100 MB
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddProduct([FromForm] AddProductDTO productDTO) 
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                await work.ProductRepository.AddAsync(productDTO);
                return Ok(new ResponseAPI(200, "Product added successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }

        /// <summary>
        /// Update existing product
        /// </summary>
        [HttpPut("update-product")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateProduct([FromForm] UpdateProductDTO updateProductDTO) 
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                await work.ProductRepository.UpdateAsync(updateProductDTO);
                return Ok(new ResponseAPI(200, "Product updated successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }

        /// <summary>
        /// Delete product
        /// </summary>
        [HttpDelete("delete-product/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProduct(int id) 
        {
            try
            {
                var product = await work.ProductRepository.GetByIdAsync(id, x => x.photos, x => x.category);
                if (product is null)
                    return NotFound(new ResponseAPI(404, "Product not found"));
                
                await work.ProductRepository.DeleteAsync(product);
                return Ok(new ResponseAPI(200, "Product deleted successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
    }
}
