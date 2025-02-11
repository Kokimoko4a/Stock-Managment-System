

namespace Stock_Managment_System.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SMS.Dtos.Driver;
    using SMS.Services.Interfaces;

    [Route("/Driver")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService driverService;

        public DriverController(IDriverService driverService)
        {
            this.driverService = driverService;
        }



        [HttpPost("becomeDriver")]
        public async Task<IActionResult> BecomeDriver([FromBody] DriverImportDto driverImportDto)
        {
            var id = GetTokenAndIdIfExists();

            if (id == null)
            {
                throw new Exception();
            }

            driverImportDto.Id = id;

            await driverService.BecomeDriver(driverImportDto);

            return Ok();
        }


        [HttpGet("driverDashboard")]
        public async Task<IActionResult> IsDriver()
        {
            var id = GetTokenAndIdIfExists();

            if (id == null)
            {
                return BadRequest();
            }


            bool result = await driverService.IsDriver(id);

            if (result)
            {
                return Ok();
            }

            return Unauthorized();
        }

        [HttpGet("getDriversForCompany")]

        public async Task<IActionResult> GetDriversForCompany([FromQuery] string companyId)
        {
            var id = GetTokenAndIdIfExists();

            if (id == null)
            {
                return BadRequest();
            }


            var drivers = await driverService.GetDriversForCompany(companyId);

            return Ok(drivers);
        }


        [HttpGet("getDetailsForDriver")]

        public async Task<IActionResult> GetDetailsForDriver([FromQuery] string driverId)
       {
            var id = GetTokenAndIdIfExists();

            if (id == null)
            {
                return BadRequest();
            }

            return Ok(await driverService.GetDetailsForDriverByCompanyId(driverId));
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
