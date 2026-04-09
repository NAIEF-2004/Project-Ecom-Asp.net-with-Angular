using Ecom_Core.DTO;
using Ecom_Core.Entites.Prudact;
using Ecom_Infrasteucture.Data.updateMigration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecom_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost("Regestore")]
        public async Task<IActionResult> Regestore(dtoRegestoreUser user)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new()
                {
                    UserName = user.Name,
                    Email = user.Email,
                };
                IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);
                if (result.Succeeded)
                {
                    return Ok("sccses");
                }
                return BadRequest(result.Errors);
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
                    if (await _userManager.CheckPasswordAsync(user, loginUser.password))
                    {
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        var rols = await _userManager.GetRolesAsync(user);
                        foreach (var rol in rols)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, rol));
                        }
                        //signingCredentials
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
                        var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                        claims: claims,
                        issuer: configuration["JWT:Issuer"],
                        audience: configuration["JWT:Audience"],
                        expires: DateTime.Now.AddHours(3),
                        signingCredentials: sc

                        );
                        var _token = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            exception = token.ValidTo,
                        };
                        return Ok(_token);
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "the user no find ");
                }
            }
            return BadRequest();
        }
    }
}
