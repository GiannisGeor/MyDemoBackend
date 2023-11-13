using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entities;
using Models.Projections;

namespace Services.Interfaces
{
    public interface IAddressRepository
    {
        Task<List<Address>> GetCustomerAddress(int customerId);

        Task<Address> AddNewAddress(Address candidate);

        Task<Address> EditAddress(Address candidate);

        Task<Address> GetAddressTrackedById(int id);

        Task<Address> GetAddressUnTrackedById(int id);

        Task<Address> GetAddressId(int customerId);

        Task<int?> GetCustomerIdByAddressId(int addressId);

        Task<Address> DeleteAddress(Address candidate);

    }
}
