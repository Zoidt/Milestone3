using Ecommerce.Api.Products.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool isSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductsAsync();
        Task<(bool isSuccess, Product product, string ErrorMessage)> GetProductAsync(int id);
    }
}
