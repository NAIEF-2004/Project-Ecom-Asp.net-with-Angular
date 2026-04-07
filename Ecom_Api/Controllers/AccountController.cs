using Ecom_Core.DTO;
using Ecom_Core.Entites.Prudact;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecom_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
         _userManager = userManager;
        }

        [HttpPost("Regestore")]
        public async Task<IActionResult> Regestore(dtoRegestoreUser user)
        {
            if (ModelState.IsValid) 
            {
                AppUser appUser = new() 
                {
                UserName=user.Name,
                Email=user.Email,
                };
                IdentityResult result=await _userManager.CreateAsync(appUser,user.Password);
                if (result.Succeeded)
                {
                    return Ok("sccses");
                }
                return BadRequest();
            }
            return BadRequest(ModelState);  
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(dtoLoginUser loginUser) 
        {
            if (ModelState.IsValid)
            {
                AppUser? user = await _userManager.FindByNameAsync(loginUser.Name);
                if (user != null)
                {
                    if (await _userManager.CheckPasswordAsync(user,loginUser.password))
                    {
                        return Ok("token");
                    }
                    else 
                    {
                        return Unauthorized();
                    }
                }
                else 
                {
                    ModelState.AddModelError("","the user no find ");
                }
            }
            return BadRequest();
        }
          

       
        }
    }

