using Ecommerce.Api.Search.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;
        private readonly ICustomersService customersService;

        public SearchService(IOrdersService ordersService, IProductsService productsService, ICustomersService customersService)
        {
            this.ordersService = ordersService;
            this.productsService = productsService;
            this.customersService = customersService;
        }

        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerID)
        {
            var ordersResult = await ordersService.GetOrdersAsync(customerID);
            var productsResult = await productsService.GetProductsAsync();
            var customersResult = await customersService.GetCustomerAsync(customerID);

            if (ordersResult.IsSuccess)
            {
                foreach (var order in ordersResult.Orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productsResult.IsSuccess ?  
                            productsResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name : 
                            "Product information is not available";
                    }
                }

                var result = new
                {
                    Customer = customersResult.IsSuccess ?
                        customersResult.Customer : // if result is true
                        new { Name = "Customer information is not available"}, // else if false
                    Orders = ordersResult.Orders,
                };

                return (true, result);
            }
            return (false, null);
        }
    }
}
