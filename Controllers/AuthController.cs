using Identity.Management.Api.Models.Authentication.SignUp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Management.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration,
            RoleManager<IdentityRole> roleManager
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterUser registerUser, string role)
        {
            // Check user exists
            var userExists = await _userManager.FindByEmailAsync(registerUser.Email);

            if (userExists != null)
            {
                return BadRequest("User already exists");
            }
            // Add the user to the database
            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email
            };

            var roleExists = await _roleManager.FindByNameAsync(role);

            if (roleExists == null)
            {
                return BadRequest("Role does not exist");
            }

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (!result.Succeeded)
            {
                return BadRequest("User creation failed");
            }
            // Assign the user to a role
            await _userManager.AddToRoleAsync(user, role);
            return Ok("User created successfully");

        }

    }
}
