using AutoMapper;
using Ecommerce.Api.Orders.Db;
using Ecommerce.Api.Orders.Interfaces;
using Ecommerce.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        public OrdersDbContext _dbContext { get; }
        public ILogger<OrdersProvider> _logger { get; }
        public IMapper _mapper { get; }

        public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            // id, custoemr id, order date, total, items: Order items
            if (!_dbContext.Orders.Any())
            {
                // First Order - order items
                List<Db.OrderItem> orderItems = new List<Db.OrderItem>
                {
                    new Db.OrderItem() { Id = 1, OrderId = 1, ProductId = 1, Quantity= 10, UnitPrice= 10},
                    new Db.OrderItem() { Id = 2, OrderId = 2, ProductId = 2, Quantity= 1, UnitPrice= 250}
                };
                var total = orderItems.Sum(item => item.Quantity * item.UnitPrice); 
                _dbContext.Orders.Add(new Db.Order()
                {

                    Id = 1,
                    CustomerId = 1,
                    OrderDate = DateTime.Now,
                    Total = total,
                    Items = orderItems
                });
                _dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var orders = await _dbContext.Orders.ToListAsync();
                if (orders != null && orders.Any())
                {
                    var result = _mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
