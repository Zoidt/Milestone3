using Ecommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.Api.Orders.Controllers
{
    /*
     * Course: 		Web Programming 3
     * Assessment: 	Milestone 3
     * Created by: 	ZAID TABANA - 2043269
     * Date: 		    9 December 2023
     * Class Name: 	OrdersController.cs
     * Description:   Controller class can recieve HTTP requests to api/orders/{customerId}
     *                Controller currently only supports handling of requests of customer Ids.
     *                Controller returns relevant information to the request
     */
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider ordersProvider;

        public OrdersController(IOrdersProvider ordersProvider)
        {
            this.ordersProvider = ordersProvider;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrdersAsync(int customerId)
        {
            var result = await ordersProvider.GetOrdersAsync(customerId);
            if(result.IsSuccess)
            {
                return Ok(result.Orders);
            }
            return NotFound();
        }
    }
}
