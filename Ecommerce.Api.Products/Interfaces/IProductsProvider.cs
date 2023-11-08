using Ecommerce.Api.Products.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool isSuccess, IEnumerable<Product> Products, string ErrorMessage)> getProductsAsync();
    }
}
