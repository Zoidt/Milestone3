using Ecommerce.Api.Search.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;

        public SearchService(IOrdersService ordersService, IProductsService productsService)
        {
            this.ordersService = ordersService;
            this.productsService = productsService;
        }

        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerID)
        {
            var ordersResult = await ordersService.GetOrdersAsync(customerID);
            var productsResult = await productsService.GetProductsAsync();

            if (ordersResult.IsSuccess)
            {
                foreach (var order in ordersResult.Orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productsResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name;
                    }
                }
                var result = new
                {
                    Orders = ordersResult.Orders,
                };

                return (true, result);
            }
            return (false, null);
        }
    }
}
