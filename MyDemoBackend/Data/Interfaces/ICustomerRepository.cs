using Models.Entities;

namespace Data.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetActiveCustomerAsync(int id);
    }
}
