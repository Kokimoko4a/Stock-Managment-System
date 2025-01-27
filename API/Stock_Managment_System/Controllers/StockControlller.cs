

namespace Stock_Managment_System.Controllers
{

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using SMS.Dtos.Stock;
    using SMS.Services;
    using SMS.Services.Interfaces;

    [Route("/Stock")]
  
    public class StockControlller : ControllerBase
    {
        private readonly IStockService stockService;

        public StockControlller(IStockService stockService)
        {
            this.stockService = stockService;
        }

        
        [HttpPost("addStock")]
        public async Task<IActionResult> AddStock([FromBody] StockImportDto stockImportDto)
        {
            var token = GetTokenAndIdIfExists();


            if (token == null)
            {
                return Unauthorized();
            }

            await stockService.CreateStock(stockImportDto);


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
