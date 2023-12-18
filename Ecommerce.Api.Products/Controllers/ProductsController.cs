using Ecommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Controllers
{
    /*
      * Course: 		Web Programming 3
      * Assessment: 	Milestone 3
      * Created by: 	ZAID TABANA - 2043269
      * Date: 		    17 - 11 - 2023
      * Class Name: 	ProductsController.cs
      * Description: 	Class handles HTTP GET Requests for products through route api/products.
      *                 Currently only supports GET requests for all products or 
      *                 for specific products using a product ID. 
      */
    [ApiController]
    [Route("api/products")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase   
    {
        private readonly IProductsProvider productsProvider;

        public ProductsController(IProductsProvider productsProvider)
        {
            this.productsProvider = productsProvider;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>RIActionResult</returns>
        /// <response code="200">Returns all products</response>
        /// <remarks>
        /// Sample request:
        /// <code>
        /// GET /products/1
        /// {
        ///     {
        ///         "id": 1,
        ///         "name": "Keyboard",
        ///         "price": 20,
        ///         "inventory": 100
        ///     },
        ///     {
        ///         "id": 2,
        ///         "name": "mouse",
        ///         "price": 10,
        ///         "inventory": 100
        ///     },
        ///     {
        ///         "id": 3,
        ///         "name": "Usb",
        ///         "price": 2,
        ///         "inventory": 1000
        ///     }
        /// }
        /// </code>
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductsAsync()
        {
            var result = await productsProvider.GetProductsAsync();

            if (result.IsSuccess)
            {
                return Ok(result.Products);
            }
            return NotFound();
        }

        /// <summary>
        /// Get product by the provided id.
        /// </summary>
        /// <param name="id">of the product</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Returns the requested product</response>
        /// <remarks>
        /// Sample request:
        /// 
        ///
        /// <code>        
        /// GET /products/1
        /// {
        ///     {
        ///         "id": 1,
        ///         "name": "Keyboard",
        ///         "price": 20,
        ///         "inventory": 100
        ///     }
        /// }
        /// </code>
        /// </remarks>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductAsync(int id)
        {
           var result = await productsProvider.GetProductAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result.Product);
            }
            return NotFound();
        }


    }
}
