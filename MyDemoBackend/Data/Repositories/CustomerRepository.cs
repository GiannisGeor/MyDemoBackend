using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DemoBackendDbContext _context;

        private IQueryable<Customer> _customerQuery;

        public CustomerRepository(DemoBackendDbContext context)
        {
            _context = context;
            _customerQuery = _context.Customer;
        }

        public async Task<Customer> GetActiveCustomerAsync(int id)
        {
            return await _customerQuery.AsNoTracking()
                .Where(x => x.IsActive && x.Id == id)
                .SingleOrDefaultAsync();
        }
    }
}
