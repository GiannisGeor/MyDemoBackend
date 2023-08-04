using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DemoBackendDbContext _customerContext;

        private IQueryable<Customer> _customerQuery;

        public CustomerRepository(DemoBackendDbContext context)
        {
            _customerContext = context;
            _customerQuery = _customerContext.Customer;
        }

        public async Task<Customer> GetActiveCustomerAsync(int id)
        {
            return await _customerQuery.AsNoTracking()
                .Where(x => x.IsActive && x.Id == id)
                .SingleOrDefaultAsync();
        }
    }
}
