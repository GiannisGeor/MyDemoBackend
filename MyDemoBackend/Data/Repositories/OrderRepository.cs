using Data.Interfaces;
using Models.Entities;

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

