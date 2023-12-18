using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.Api.Search.Controllers
{
    /*
      * Course: 		Web Programming 3
      * Assessment: 	Milestone 3
      * Created by: 	ZAID TABANA - 2043269
      * Date: 		    9 December 2023
      * Class Name: 	SearchController.cs
      * Description: 	Class handles POST Requests for search terms.
      *                 This controller is central for other microservices (Customers, Products, Orders)
      *                 Currently requires a customer ID and will fetch all relevant info in the other
      *                 microservices used in the project.
      */
    [ApiController]
    [Route("api/search")]
    [Produces("application/json")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        /// <summary>
        /// Get search result using customer id provided
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <response code="200">Returns search term</response>
        /// <remarks>
        /// Sample request:
        /// <code>
        /// POST /search
        /// {
        ///     "customer": {
        ///         "id": 1,
        ///         "name": "John Doe",
        ///         "address": "102 rue barceloone"
        ///     },
        ///     "orders": [
        ///         {
        ///             "id": 1,
        ///             "orderDate": "2023-12-17T21:44:20.2343963-05:00",
        ///             "total": 100,
        ///             "items": [
        ///                 {
        ///                     "id": 1,
        ///                     "productId": 1,
        ///                     "productName": "Keyboard",
        ///                     "quantity": 10,
        ///                     "unitPrice": 10
        ///                 },
        ///                 {
        ///                     "id": 2,
        ///                     "productId": 2,
        ///                     "productName": "Mouse",
        ///                     "quantity": 10,
        ///                     "unitPrice": 10
        ///                 },
        ///                 {
        ///                     "id": 3,
        ///                     "productId": 3,
        ///                     "productName": "Monitor",
        ///                     "quantity": 10,
        ///                     "unitPrice": 10
        ///                 },
        ///                 {
        ///                     "id": 4,
        ///                     "productId": 2,
        ///                     "productName": "Mouse",
        ///                     "quantity": 10,
        ///                     "unitPrice": 10
        ///                 },
        ///                 {
        ///                     "id": 5,
        ///                     "productId": 3,
        ///                     "productName": "Monitor",
        ///                     "quantity": 1,
        ///                     "unitPrice": 100
        ///                 }
        ///             ]
        ///         }
        ///     ]
        /// }
        /// </code>
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SearchAsync(SearchTerm term)
        {
            var result = await searchService.SearchAsync(term.CustomerID);
            if(result.IsSuccess)
            {
                return Ok(result.SearchResults);
            }
            return NotFound();
        }
    }
}
