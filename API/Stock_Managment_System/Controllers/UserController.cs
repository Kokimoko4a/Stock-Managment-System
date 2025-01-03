namespace Stock_Managment_System.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SMS.Dtos;
    using SMS.Models;
    using SMS.Services.Interfaces;

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
        public IActionResult Login([FromBody] string test)
        {
            return Ok("Super hi  " + test);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {


            var user = userService.CreateUser(registerDTO);


            await userManager.SetEmailAsync(user, registerDTO.Email);
            await userManager.SetUserNameAsync(user, registerDTO.Email);
            IdentityResult result =
                await userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            await signInManager.SignInAsync(user, false);


            return Ok("Successfully created profile!");






        }
    }
}
