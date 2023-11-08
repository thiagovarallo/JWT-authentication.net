using JWT_authentication.Data.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JWT_authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public UserManager<IdentityUser> UserManager { get; }

        public AuthController(UserManager<IdentityUser> userManager)
        {
            UserManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto )
        {
            var identityUser = new IdentityUser
            {
                UserName = dto.Username,
                Email = dto.Username
            };

            var identityResult = await UserManager.CreateAsync(identityUser, dto.Password);

            if (identityResult.Succeeded)
            {
                // add roles to this User
                if (dto.Roles != null && dto.Roles.Any() ) 
                {
                   identityResult = await UserManager.AddToRolesAsync(identityUser, dto.Roles);
                
                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login :)");
                    }
                }
            }

            return BadRequest("Something went wrong");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO dto)
        {
            var user = await UserManager.FindByEmailAsync(dto.Username);

            if (user != null)
            {
                var checkPassword = await UserManager.CheckPasswordAsync(user, dto.Password);
            
                if (checkPassword)
                {
                    //create token

                    return Ok();
                }
            }

            return BadRequest("Username or password incorrect");
        }
    }
}
