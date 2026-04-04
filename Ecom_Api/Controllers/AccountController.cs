using Ecom_Core.DTO;
using Ecom_Core.Entites.Prudact;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecom_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }


        [HttpPost]
        public async Task<IActionResult> Register(AccountUser user)
        {
            if (ModelState.IsValid)
            {
                AppUser appuser = new()
                {
                  UserName=user.Name,
                  Email=user.Email,
                  

                };

                IdentityResult result = await userManager.CreateAsync(appuser);
            }


            return BadRequest();
        }
    }
}
