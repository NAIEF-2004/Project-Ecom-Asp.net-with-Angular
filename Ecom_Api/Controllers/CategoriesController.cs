using AutoMapper;
using Ecom_Api.Helper;
using Ecom_Core.DTO;
using Ecom_Core.Entites.Prudact;
using Ecom_Core.Interface;
using Ecom_Infrasteucture.Reposetores;
using Microsoft.AspNetCore.Authorization;
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

        public CategoriesController(IUnitOfWork work, IMapper map)
        {
            this.work = work;
            this.map = map;
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        [HttpGet("get-all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAllCategories() 
        { 
            try
            {
                var categories = await work.CategoryRepository.GetAllAsync();
                if (categories is null || !categories.Any())
                    return NotFound(new ResponseAPI(404, "No categories found"));
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));  
            }  
        }
        
        /// <summary>
        /// Get category by ID
        /// </summary>
        [HttpGet("get-by-id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var category = await work.CategoryRepository.GetByIdAsync(id);
                if (category is null) 
                    return NotFound(new ResponseAPI(404, "Category not found"));
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }

        /// <summary>
        /// Add new category
        /// </summary>
        [HttpPost("add-category")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddCategory(CategoryDTO categoryDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                var category = map.Map<Category>(categoryDto);
                await work.CategoryRepository.AddAsync(category);
                return Ok(new ResponseAPI(200, "Category added successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }

        /// <summary>
        /// Update category
        /// </summary>
        [HttpPut("update-category")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateCategory(UpdateCategoryDTO updateCategoryDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                var category = map.Map<Category>(updateCategoryDto);
                await work.CategoryRepository.UpdateAsync(category);
                return Ok(new ResponseAPI(200, "Category updated successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }

        /// <summary>
        /// Delete category
        /// </summary>
        [HttpDelete("delete-category/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteCategory(int id) 
        {
            try
            {
                await work.CategoryRepository.DeleteAsync(id);
                return Ok(new ResponseAPI(200, "Category deleted successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
    }
}
