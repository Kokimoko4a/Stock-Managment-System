
namespace Stock_Managment_System.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using SMS.Dtos.Vehicles;
    using SMS.Services.Interfaces;

    [Route("/Vehicle")]

    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        [HttpPost("addVehicle")]
        public async Task<IActionResult> CreateVehicle([FromForm] VehicleDtoImport vehicleDto)
        {
            string id = GetTokenAndIdIfExists();

            if (id == null)
            {
                return BadRequest();
            }

          

            await vehicleService.CreateVehicle(vehicleDto);

            return Ok();
        }

        private string GetTokenAndIdIfExists()
        {
            var token = Request.Headers["Authorization"].ToString();


            if (token == null)
            {
                return null;
            }



            string id = token.Remove(0, 6).Trim();

            return id;
        }

    }
}
