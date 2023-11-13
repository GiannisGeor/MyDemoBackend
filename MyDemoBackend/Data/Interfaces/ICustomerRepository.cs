using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entities;

namespace Data.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> AddNewCustomer(Customer customer);

        Task<Customer> GetCustomerByEmail(string email);
    }
}
