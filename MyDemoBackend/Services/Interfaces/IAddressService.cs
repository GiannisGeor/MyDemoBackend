using Messages;
using Models.Projections;
using Services.Dtos;

namespace Services.Interfaces
{
    public interface IAddressService
    {

        Task<ListResponse<CustomerAddressDto>> GetCustomerAddress();

        Task<ObjectResponse<AddressResponseDto>> NewAddress(AddressDto newAddress);
        Task<ObjectResponse<AddressResponseDto>> EditAddress(int id, AddressDto EditAddress);
        Task<ValueResponse<bool>> DeleteAddress(int id);
    }
}
