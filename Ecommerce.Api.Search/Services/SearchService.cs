using Ecommerce.Api.Search.Interfaces;
using System.Threading.Tasks;

namespace Ecommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;

        public SearchService(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerID)
        {
            var ordersResult = await ordersService.GetOrdersAsync(customerID);
            if (ordersResult.IsSuccess)
            {
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
