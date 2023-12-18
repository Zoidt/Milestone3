using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Models;
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

        [HttpPost]
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
