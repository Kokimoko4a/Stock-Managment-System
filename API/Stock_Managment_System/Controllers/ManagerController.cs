

namespace Stock_Managment_System.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SMS.Services.Interfaces;
    using System.Security.Claims;



    // [Authorize]
    [Route("/Manager")]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService managerService;

        public ManagerController(IManagerService managerService)
        {
            this.managerService = managerService;
        }

        [HttpPost("becomeManager")]
        public async Task<IActionResult> BecomeManager()
        {
            var token = Request.Headers["Authorization"].ToString();


            if (token == null)
            {
                return Unauthorized();
            }



            string id = token.Remove(0, 6);


            await managerService.BecomeManager(id.Trim());

            return Ok();
        }
    }
}
