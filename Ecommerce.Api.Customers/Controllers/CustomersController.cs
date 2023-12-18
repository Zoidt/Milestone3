using Ecommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.Api.Customers.Controllers
{
        /*
      * Course: 		Web Programming 3
      * Assessment: 	Milestone 3
      * Created by: 	ZAID TABANA - 2043269
      * Date: 		    17 - 11 - 2023
      * Class Name: 	CustomersController.cs
      * Description: 	This controller is responsible for handling Customer/s requests
      *                 Currently only supports GET Requests for all or specific customer IDs
      */
    [ApiController]
    [Route("api/customers")]
    [Produces("application/json")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersProvider customersProvider;

        public CustomersController(ICustomersProvider customersProvider)
        {
            this.customersProvider = customersProvider;
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <response code="200">Returns customers</response>
        /// <remarks>
        /// Sample request:
        /// <code>
        /// GET /customers
        /// {
        ///     {
        ///         "id": 1,
        ///         "name": "Lebron James",
        ///         "Address": "212 G Street",
        ///     },
        ///     {
        ///         "id": 2,
        ///         "name": "Kratos",
        ///         "Address": "Valhalla",
        ///     },
        ///     {
        ///         "id": 3,
        ///         "name": "Peter Parker",
        ///         "Address": "Symbiote City",
        ///     }
        /// }
        /// </code>
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await customersProvider.GetCustomersAsync();
            if(result.IsSuccess)
            {
                return Ok(result.Customers);
            }
            return NotFound();
        }

        /// <summary>
        /// Get customer with provided id
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <response code="200">Returns customer</response>
        /// <remarks>
        /// Sample request:
        /// 
        ///
        /// <code>        
        /// GET /customers/1
        /// {
        ///     "id": 1,
        ///     "name": "Lebron James",
        ///     "Address": "212 G Street,
        /// }
        /// </code>
        /// </remarks>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomersAsync(int id)
        {
            var result = await customersProvider.GetCustomerAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Customer);
            }
            return NotFound();
        }
    }
}
