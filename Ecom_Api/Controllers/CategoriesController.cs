using AutoMapper;
using Ecom_Api.Helper;
using Ecom_Core.DTO;
using Ecom_Core.Entites.Prudact;
using Ecom_Core.Interface;
using Ecom_Infrasteucture.Reposetores;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork work;
        private readonly IMapper map;

        public CategoriesController(IUnitOfWork work,IMapper map)
        {
            this.work = work;
            this.map = map;
        }


        [HttpGet("get-all")]
        public async Task<ActionResult> GetAllCategories() 
        { 
          
            try
            {
                var categories = await work.CategoryRepostiry.GetAllAsync();
                if (categories is null)
                    return BadRequest();
                return Ok(categories);
               
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400));  
            }  

        }
        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult> Getbyid(int id)
        {
            try
            {
                var category = await work.CategoryRepostiry.GetByIdAsync(id);
                if (category is null) return BadRequest();
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400));
            }

        }
        [HttpPost("add-category")]
        public async Task<ActionResult> AddCategory(CategoryDTO categorydto)
        {
            try
            {
                var x = map.Map<Category>(categorydto);
                await work.CategoryRepostiry.AddAsync(x);
                return Ok(new { message = "ok add category" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update-category")]
        public async Task<ActionResult> Updatecategory(updatecategoryDTO updatecategore)
        {
            try
            {
                var category = map.Map<Category>(updatecategore);
                await work.CategoryRepostiry.UpdateAsync(category);
                return Ok(new { message = "item in category update" });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("delete-category/{id}")]
        public async Task<ActionResult> Deletecategory(int id) 
        {
            try
            {
                await work.CategoryRepostiry.DeleteAsync(id);
                return Ok(new { message = "item in category delete" });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
