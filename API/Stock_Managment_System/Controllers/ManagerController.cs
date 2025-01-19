

namespace Stock_Managment_System.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SMS.Dtos.Company;
    using SMS.Dtos.User;
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

        [HttpGet("managerDashboard")]
        public IActionResult DashBoardAccessing() 
        {
            string id =  GetTokenAndIdIfExists();

            if (id == null)
            {
                return BadRequest();
            }

            if (!managerService.IsManager(id))
            {
                return Unauthorized();
            }

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


        [HttpPost("createCompany")]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyDto companyDto) 
        {
            string id =  GetTokenAndIdIfExists();

            await managerService.CreateCompany(id, companyDto); 

            return Ok();
        }

        [HttpGet("getAllCompanies")]
        public async Task<IActionResult> GetAllCompanies() 
        {
            string id = GetTokenAndIdIfExists();

            var companies =  await managerService.GetAllCompanies(id);

            if (companies == null)
            { 
                return NotFound();  
            }

            return Ok(companies);
        }

        [HttpPut("updateCompany")]

        public async Task<IActionResult> UpdateCompany([FromBody] CompanyDtoEditImport companyDtoEditImport)
        {
            string userId = GetTokenAndIdIfExists();

            var companies =  await managerService.UpdateCompany(companyDtoEditImport,userId);

            if (companies == null)
            {
                return NotFound();
            }

            return Ok(companies);   

        }

        [HttpPost("getCompany")]
        public async Task<IActionResult> GetCompany([FromBody] CompanySmallExport companySmallExport)
        {
            if (GetTokenAndIdIfExists() == null)
            {
                return BadRequest();
            }


            var company = await managerService.GetCompany(companySmallExport.Id);


            return Ok(company);
        }

        [HttpGet("getDetailedCompany/{id}")]

        public async Task<IActionResult> GetDetailedCompany([FromRoute]string  id) 
        {
            if (GetTokenAndIdIfExists() == null)
            {
                return BadRequest();
            }

            var company = await managerService.GetCompany(id.ToString());

            return Ok(company);
        }

        [HttpDelete("deleteCompany/{id}")]

        public async Task<IActionResult> DeleteCompany([FromRoute] string id)
        {
            if (GetTokenAndIdIfExists() == null!) 
            {
                return BadRequest(); 
            }

            await managerService.DeleteCompany(id);

            Ok();
        }
    }
}
