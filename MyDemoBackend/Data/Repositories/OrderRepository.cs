using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Models.Entities;
using Models.Enums;

namespace Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DemoBackendDbContext _context;

        private IQueryable<Order> _orderQuery;

        public OrderRepository(DemoBackendDbContext context)
        {
            _context = context;
            _orderQuery = _context.Order;
        }

        public async Task<Order> AddNewOrder(Order candidate)
        {
            await _context.AddAsync(candidate);
            await _context.SaveChangesAsync();
            return candidate;
        }

    }

}

