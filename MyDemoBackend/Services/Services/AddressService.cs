using AutoMapper;
using Common.Methods;
using Data.Interfaces;
using FluentValidation;
using Messages;
using Microsoft.AspNetCore.Http;
using Models.Entities;
using Models.Enums;
using Serilog;
using Services.Dtos;
using Services.Interfaces;
namespace Services.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _addressmapper;
        private readonly IValidator<AddressDto> _newAddressValidator;
        private readonly IValidator<AddressDto> _editAddressValidator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddressService(
            IAddressRepository addressRepository,
            IHttpContextAccessor httpContextAccessor,
            IValidator<AddressDto> newAddressValidator,
            IValidator<AddressDto> editAddressValidator,
            IMapper addressmapper)
        {
            _addressRepository = addressRepository;
            _httpContextAccessor = httpContextAccessor;
            _newAddressValidator = newAddressValidator;
            _editAddressValidator = editAddressValidator;
            _addressmapper = addressmapper;
        }

        public async Task<ListResponse<CustomerAddressDto>> GetCustomerAddress()
        {
            ListResponse<CustomerAddressDto> response = new ListResponse<CustomerAddressDto>();
            try
            {
                int customerId = GlobalMethods.GetAppropriateCustomerId(_httpContextAccessor.HttpContext.User);

                var customerAddress = await _addressRepository.GetCustomerAddress(customerId);
                var dtoAfterMapping = _addressmapper.Map<List<CustomerAddressDto>>(customerAddress);
                foreach ( CustomerAddressDto addressdto in dtoAfterMapping )
                {
                    addressdto.CanBeDeleted = AddressCanBeDeleted(customerAddress.FirstOrDefault(x => x.Id == addressdto.Id));
                }
                response.SetSuccess(dtoAfterMapping);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetCustomerAddress with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetCustomerAddress with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }
        }

        public async Task<ObjectResponse<AddressResponseDto>> NewAddress(AddressDto addressDto)
        {
            ObjectResponse<AddressResponseDto> response = new ObjectResponse<AddressResponseDto>();
            try
            {
                var validationResult = _newAddressValidator.Validate(addressDto);
                if (!validationResult.IsValid)
                {
                    response.SetFailureWithValidation(validationResult);
                    return response;
                }

                int customerId = GlobalMethods.GetAppropriateCustomerId(_httpContextAccessor.HttpContext.User);
                string customerEmail = GlobalMethods.GetAppropriateCustomerEmail(_httpContextAccessor.HttpContext.User);

                Address candidate = new Address();
                candidate.FullAddress = addressDto.FullAddress;
                candidate.PostalCode = addressDto.PostalCode;
                candidate.Type = AddressType.Customer;
                candidate.Floor = addressDto.Floor;
                candidate.DoorbellName = addressDto.DoorbellName;
                candidate.CustomerId = customerId;
                candidate.MarkNew(customerEmail);

                Address entity = await _addressRepository.AddNewAddress(candidate);
                var dtoAfterMapping = _addressmapper.Map<AddressResponseDto>(entity);
                response.SetSuccess(dtoAfterMapping);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing NewAddress with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing NewAddress with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }
        }

        public async Task<ObjectResponse<AddressResponseDto>> EditAddress(int id, AddressDto addressDto)
        {
            ObjectResponse<AddressResponseDto> response = new ObjectResponse<AddressResponseDto>();
            try
            {
                var validationResult = _editAddressValidator.Validate(addressDto);
                if (!validationResult.IsValid)
                {
                    response.SetFailureWithValidation(validationResult);
                    return response;
                }

                int customerId = GlobalMethods.GetAppropriateCustomerId(_httpContextAccessor.HttpContext.User);
                string customerEmail = GlobalMethods.GetAppropriateCustomerEmail(_httpContextAccessor.HttpContext.User);


                Address candidate = await _addressRepository.GetAddressUnTrackedById(id);
                if (candidate == null)
                {
                    response.SetHttpFailureCode($@"This address does not exist", HttpResultCode.NotFound);
                    return response;
                }

                candidate.FullAddress = addressDto.FullAddress;
                candidate.PostalCode = addressDto.PostalCode;
                candidate.Type = AddressType.Customer;
                candidate.Floor = addressDto.Floor;
                candidate.DoorbellName = addressDto.DoorbellName;
                candidate.CustomerId = customerId;
                candidate.MarkDirty(customerEmail);

                Address entity = await _addressRepository.EditAddress(candidate);
                var dtoAfterMapping = _addressmapper.Map<AddressResponseDto>(entity);
                response.SetSuccess(dtoAfterMapping);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing EditAddress with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing EditAddress with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }
        }

        public async Task<ValueResponse<bool>> DeleteAddress(int id)
        {
            ValueResponse<bool> response = new ValueResponse<bool>();
            try
            {

                int customerId = GlobalMethods.GetAppropriateCustomerId(_httpContextAccessor.HttpContext.User);
                string customerEmail = GlobalMethods.GetAppropriateCustomerEmail(_httpContextAccessor.HttpContext.User);


                Address candidate = await _addressRepository.GetAddressUnTrackedById(id);
                if (candidate == null)
                {
                    response.SetHttpFailureCode($@"This address does not exist", HttpResultCode.NotFound);
                    return response;
                }

                //Checks
                if (candidate.CustomerId != customerId || !AddressCanBeDeleted(candidate))
                {
                    response.SetHttpFailureCode($@"Address cannot be deleted", HttpResultCode.Forbidden);
                    return response;
                }
               
                candidate.MarkDeleted(customerEmail);

                await _addressRepository.DeleteAddress(candidate);
                response.SetSuccess(true);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing DeleteAddress with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing DeleteAddress with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        private bool AddressCanBeDeleted(Address address)
        {
            return address.Order.Count == 0;
        }
    }
}
