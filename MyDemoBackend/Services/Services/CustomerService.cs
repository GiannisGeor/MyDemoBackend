using AutoMapper;
using Data.Interfaces;
using Messages;
using Models.Entities;
using Serilog;
using Services.Dtos;
using Services.Interfaces;

namespace Services.Services
{
    public class CustomerService : ICustomerService
    {
        ICustomerRepository _customerRepository;
        IMapper _customerMapper;

        public CustomerService(
            ICustomerRepository customerRepository,
            IMapper customerMapper)
        {
            _customerRepository = customerRepository;
            _customerMapper = customerMapper;
        }

        public async Task<ObjectResponse<CustomerDto>> GetActiveCustomer(int id)
        {
            ObjectResponse<CustomerDto> response = new ObjectResponse<CustomerDto>();
            try
            {
                Customer customer = await _customerRepository.GetActiveCustomerAsync(id);
                if (customer == null)
                {
                    response.SetHttpFailureCode($@"CUstomer does not exist or is not active", HttpResultCode.NotFound);
                    return response;
                }

                var dtoAfterMapping = _customerMapper.Map<CustomerDto>(customer);

                response.SetSuccess(dtoAfterMapping);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing Get Customer with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing Get Customer with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }
        }
    }
}
