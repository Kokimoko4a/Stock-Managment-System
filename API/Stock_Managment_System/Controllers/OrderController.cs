

namespace Stock_Managment_System.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using SMS.Dtos.Order;
    using SMS.Services.Interfaces;



    [Route("/Order")]

    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }


        [HttpPost("createOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderImportDto orderImportDto)
        {
            string id = GetTokenAndIdIfExists();

            if (id == null)
            {
                return BadRequest();
            }


            await orderService.CreateOrder(orderImportDto);

            return Ok();

        }


        [HttpGet("getOrdersForCompany")]

        public async Task<IActionResult> GetOrders([FromQuery] string companyId)
        {

            string id = GetTokenAndIdIfExists();

            if (id == null)
            {
                return BadRequest();
            }

            return Ok(await orderService.GetOrdersByCompanyId(companyId));
        }



        [HttpGet("getDetailedOrder")]

        public async Task<IActionResult> GetDetailedOrder([FromQuery] string orderId)
        {
            string id = GetTokenAndIdIfExists();

            if (id == null)
            {
                return BadRequest();
            }

            return Ok(await orderService.GetDetailedOrderById(orderId));

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
