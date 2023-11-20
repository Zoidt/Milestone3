using Ecommerce.Api.Search.Interfaces;
using System.Threading.Tasks;

namespace Ecommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {

        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerID)
        {
            await Task.Delay(1);
            return (true, new { Message = "Hello" });
        }
    }
}
