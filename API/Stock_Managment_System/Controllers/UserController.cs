

namespace Stock_Managment_System.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;
   // using BCrypt.Net.BCrypt;
    using SMS.Dtos.User;
    using SMS.Models;
    using SMS.Services.Interfaces;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    [ApiController]
    [Route("/User")]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService userService;

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _logger = logger;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.userService = userService;
        }

        [HttpPost("login")]
        public  IActionResult Login([FromBody] LoginDto loginDto)
        {
            
            ApplicationUser user = userService.GetUserByEmail(loginDto.Email);

            var passwordHasher = new PasswordHasher<ApplicationUser>();

            if (user == null || passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password) == PasswordVerificationResult.Failed)
            {
                return Unauthorized(new { Message = "Invalid email or password." });
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Email, user.Email),
    };

            // Create signing credentials with a symmetric key (make sure to keep the secret key safe)
            var key = Encoding.UTF8.GetBytes("your-secure-key-that-is-at-least-16-bytes"); // 128 bits
            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            // Generate the JWT token (set expiration and other token parameters)
            var token = new JwtSecurityToken(
                issuer: "YourIssuer", // e.g., "MyApp"
                audience: "YourAudience", // e.g., "MyAppClients"
                claims: claims,
                expires: DateTime.Now.AddHours(1), // Token expiration time
                signingCredentials: creds
            );

            // Return the token as part of the response
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = user.Id;

            return Ok(new { Token = tokenString });
        }


        [HttpGet("logout")]
        public  IActionResult Logout()
        {
            //await signInManager.SignOutAsync();



            return Ok( new { message  =  "Logged out successfully." });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {

            var user = userService.CreateUser(registerDTO);

            // Step 2: Set email and username
            await userManager.SetEmailAsync(user, registerDTO.Email);
            await userManager.SetUserNameAsync(user, registerDTO.Email);

            // Step 3: Create the user in the system
            IdentityResult result = await userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new { message = "User registration failed", details = result.Errors });
            }

            // Step 4: Generate JWT Token after user is created
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) // Optional, for user ID
    };

            var key = Encoding.UTF8.GetBytes("your-secure-key-that-is-at-least-16-bytes"); // Make sure to use a secure key
            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "YourIssuer",
                audience: "YourAudience",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = user.Id;

            // Step 5: Respond with the token
            return Ok(new { Token = tokenString });





        }
    }
}
