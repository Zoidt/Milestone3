using Ecommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Http;
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
    [Produces("application/json")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider ordersProvider;

        public OrdersController(IOrdersProvider ordersProvider)
        {
            this.ordersProvider = ordersProvider;
        }


        /// <summary>
        /// Get all orders with provided customer ID
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <response code="200">Returns orders</response>
        /// <remarks>
        /// Sample request:
        /// <code>
        /// GET /Orders/1
        /// 
        ///     {
        ///         "id": 1,
        ///         "CustomerId": "Keyboard",
        ///         "OrderDate": 20,
        ///         "Items": 
        ///                 [
        ///                     {
        ///                         "id": 2,
        ///                         "ProductId": "mouse",
        ///                         "Quantity": 10,
        ///                         "UnitPrice": 100
        ///                     },
        ///                     {
        ///                         "id": 3,
        ///                         "ProductId": "mouse",
        ///                         "Quantity": 10
        ///                         "UnitPrice": 100
        ///                     }
        ///                 ]
        ///     }
        /// </code>
        /// </remarks>
        [HttpGet("{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
