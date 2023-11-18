using AutoMapper;
using Ecommerce.Api.Customers.Db;
using Ecommerce.Api.Customers.Models;
using Ecommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext dbContext;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;

        public CustomersProvider(CustomersDbContext dbContext, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new Db.Customer() { Id = 1, Name = "John Doe", Address = "102 rue barceloone" });
                dbContext.Customers.Add(new Db.Customer() { Id = 2, Name = "Jane Doe", Address = "103 rue barceloone" });
                dbContext.Customers.Add(new Db.Customer() { Id = 3, Name = "Joseph Doe", Address = "104 rue barceloone" });
                dbContext.Customers.Add(new Db.Customer() { Id = 4, Name = "Bombo Doe", Address = "105 rue barceloone" });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                logger?.LogInformation("Querying customers");
                var customers = await dbContext.Customers.ToListAsync();

                if (customers != null && customers.Any())
                {
                    logger?.LogInformation($"{customers.Count} ");
                    var result = mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(customers);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<(bool IsSuccess, Models.Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                logger?.LogInformation("Querying customers");
                var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);

                if (customer != null)
                {
                    logger?.LogInformation($"{customer.Name}");
                    var result = mapper.Map<Db.Customer, Models.Customer>(customer);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {

                throw;
            }

        }

       
      
    }
}
