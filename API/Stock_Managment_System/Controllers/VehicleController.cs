
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

        [HttpGet("getAllVehicles")]
        public async Task<IActionResult> GetAllVehicles([FromQuery] string companyId)
        {
            string id = GetTokenAndIdIfExists();

            if (id == null)
            {
                return BadRequest();
            }


            return Ok(await vehicleService.GetAllVehiclesByCompanyId(companyId));


        }

        [HttpGet("getVehicleById")]
        public async Task<IActionResult> GetVehicleById([FromQuery] string vehicleId)
        {
            string id = GetTokenAndIdIfExists();

            if (id == null)
            {
                return BadRequest();
            }

            return Ok(await vehicleService.GetDetailedInfoForVehicleById(vehicleId));
        }


        [HttpPost("assignVehicleToDriver")]

        public async Task<IActionResult> AssignVehicleToDriver([FromBody] AssignVehicleToDriverDto assignVehicleToDriverDto)
        {
            string id = GetTokenAndIdIfExists();

            if (id == null)
            {
                return BadRequest();
            }


            await vehicleService.AssignVehicleToDriver(assignVehicleToDriverDto.Email, assignVehicleToDriverDto.VehicleId);

            return Ok();
        }









        [HttpPut("updateVehicle")]

        public async Task<IActionResult> UpdateVehicleById([FromForm] VehicleDtoImport vehicleDto)
        {
            string id = GetTokenAndIdIfExists();

            if (id == null)
            {
                return BadRequest();
            }

            await vehicleService.UpdateVehicle(vehicleDto);

            return Ok();
        }




        [HttpDelete("deleteVehicle")]
        public async Task<IActionResult> DeleteVehicle([FromQuery] string vehicleId)
        {
            string id = GetTokenAndIdIfExists();

            if (id == null)
            {
                return BadRequest();
            }

            await vehicleService.DeleteVehicle(vehicleId);

            return Ok();

        }



        [HttpGet("getVehicleImage")]
        public IActionResult GetVehicleImage([FromQuery] string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath) || !System.IO.File.Exists(imagePath))
            {
                return NotFound(); // Return 404 if the file doesn't exist
            }

            var imageFileStream = System.IO.File.OpenRead(imagePath);
            var mimeType = "image/jpeg"; // Adjust based on the actual image type

            return File(imageFileStream, mimeType);
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
