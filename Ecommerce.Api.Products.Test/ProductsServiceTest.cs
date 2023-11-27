using AutoMapper;
using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Profiles;
using Ecommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Threading.Tasks;
using Xunit;
using System.Text.Json;

namespace Ecommerce.Api.Products.Test
{
    public class ProductsServiceTest
    {
        [Fact]
        public async Task GetProductsReturnsAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
                .Options;

            var dbContext = new ProductsDbContext(options);


            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);


            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var products = await productsProvider.GetProductsAsync();

            Assert.True(products.IsSuccess);
            Assert.True(products.Products.Any());
            Assert.Null(products.ErrorMessage);
        }

        private void CreateProducts(ProductsDbContext dbContext)
        {
            for (var i = 0; i < 10; i++)
            {
                dbContext.Products.Add(new Product 
                {  
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = 10,
                    Price = (decimal) (i*3.14)
                });
            }
            dbContext.SaveChanges();
        }
    }
}
