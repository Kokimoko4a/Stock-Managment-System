

namespace Stock_Managment_System.Controllers
{

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
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

        [HttpGet("getStocksForCompany/{companyId}")]
        public async Task<IActionResult> GetAllStocks([FromRoute] string companyId)
        {
            var token = GetTokenAndIdIfExists();


            if (token == null)
            {
                return Unauthorized();
            }


            return Ok(await stockService.GetAllStocksForCompany(companyId));

        }

        [HttpGet("getStock/{stockId}")]

        public async Task<IActionResult> GetStock([FromRoute] string stockId)
        {
            var token = GetTokenAndIdIfExists();


            if (token == null)
            {
                return Unauthorized();
            }

            var test = await stockService.GetStock(stockId);

            return Ok(test);
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

        [HttpPut("updateStock")]
        public async Task<IActionResult> UpdateStock([FromBody] SmallStockExportDto stock)
        {
            var token = Request.Headers["Authorization"].ToString();


            if (token == null)
            {
                return BadRequest();
            }


            await stockService.UpdateStock(stock);
            

            return Ok();
        }

    }
}
