using Ecommerce.Api.Products.Interfaces;
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
      * Description: 	Routes HTTP requests to approriate web pages. 
      *                 Uses repository to update or retrieve information from an SQL database.
      *                This controller is responsible for handling Product/s requests
      */
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase   
    {
        private readonly IProductsProvider productsProvider;

        public ProductsController(IProductsProvider productsProvider)
        {
            this.productsProvider = productsProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var result = await productsProvider.GetProductsAsync();

            if (result.IsSuccess)
            {
                return Ok(result.Products);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
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
