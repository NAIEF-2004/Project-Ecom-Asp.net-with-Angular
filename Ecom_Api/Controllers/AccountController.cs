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


        [HttpPost]
        public async Task<IActionResult> Register(dtoAccountUser user)
        {
            if (ModelState.IsValid)
            {
                AppUser appuser = new()
                {
                  UserName=user.Name,
                  Email=user.Email,

                };

                IdentityResult result = await _userManager.CreateAsync(appuser,user.Password);
                if (result.Succeeded)
                {
                    return Ok("succass");
                }
                else
                {
                    //حط اخطاءك
                }
            }
          


            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> login(dtologin login) {

            if (ModelState.IsValid)
            {

                AppUser user= await _userManager.FindByNameAsync(login.Name);
                if (user != null)
                {
                    if (await _userManager.CheckPasswordAsync(user, login.password)) {
                        return Ok("token");
                    }
                    else { return Unauthorized(); }

                }
                else {
                    ModelState.AddModelError("", "model not found ");
                }
            }
            else {
               return BadRequest();
            
            }



          

       
        }
    }
}
