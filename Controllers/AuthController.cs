using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Identity.Management.Api.Dtos.Authentication.Login;
using Identity.Management.Api.Models.Authentication.SignUp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            // Checking the user
            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user == null || user.Email == null)
            {
                return Unauthorized();
            }
            // checking the password
            var result = await _userManager.CheckPasswordAsync(user, login.Password);

            if (!result)
            {
                return Unauthorized();
            }

            // claimlist creation
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // add role to claimlist
            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // generate token with the claim

            var token = GetToken(claims);

            // return token

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
        private JwtSecurityToken GetToken(List<Claim> authClaim)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"] ?? throw new InvalidOperationException()));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaim,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
    }




}
