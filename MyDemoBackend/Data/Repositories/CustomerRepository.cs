using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Enums;

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

        public async Task<Customer> AddNewCustomer(Customer customer)
        {
            await _context.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            return await _customerQuery.AsNoTracking()
                .Where(x => x.IsActive && x.Email == email).SingleOrDefaultAsync();
        }
    }
}
