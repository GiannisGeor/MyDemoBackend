using Messages;
using Services.Dtos;

namespace Services.Interfaces
{
    public interface ICustomerService
    {
        /// <summary>
        /// Returns the customer based on the Id
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns>Customer Dto</returns>
        Task<ObjectResponse<CustomerDto>> GetActiveCustomer(int id);
    }
}
