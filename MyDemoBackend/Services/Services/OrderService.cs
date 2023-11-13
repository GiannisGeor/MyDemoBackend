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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _orderMapper;
        private readonly IValidator<NewOrderDto> _newOrderValidator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IAddressRepository addressRepository,
            IMapper orderMapper,
            IHttpContextAccessor httpContextAccessor,
            IValidator<NewOrderDto> newOrderValidator)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _addressRepository = addressRepository;
            _orderMapper = orderMapper;
            _httpContextAccessor = httpContextAccessor;
            _newOrderValidator = newOrderValidator;
        }

        public async Task<ObjectResponse<NewOrderResponseDto>> NewOrder(NewOrderDto newOrderDto)
        {
            ObjectResponse<NewOrderResponseDto> response = new();
            try
            {
                int customerId = GlobalMethods.GetAppropriateCustomerId(_httpContextAccessor.HttpContext.User);

                //response.SetHttpFailureCode($@"test", HttpResultCode.InternalServerError);
                //return response;
                var validationResult = _newOrderValidator.Validate(newOrderDto);
                if (!validationResult.IsValid)
                {
                    response.SetFailureWithValidation(validationResult);
                    return response;
                }

                int? addressCustomerId = await _addressRepository.GetCustomerIdByAddressId(newOrderDto.AddressId);
                if (addressCustomerId != customerId)
                {
                    response.SetHttpFailureCode($@"The customerId does not match the existing customerId in the database", HttpResultCode.InternalServerError);
                    return response;
                }


                List <Product> candidateProducts = await _productRepository.GetIdProducts(newOrderDto.OrderLines.Select(x => x.ProductId).Distinct().ToList());

                Order candidate = new Order();
                candidate.orderStatus = OrderStatus.OrderPlaced;
                candidate.StoreId = newOrderDto.StoreId;
                candidate.AddressId = newOrderDto.AddressId;
                candidate.CustomerId = customerId;
                candidate.OrderComments = newOrderDto.OrderComments;
                candidate.MarkNew();

                //Address candidateAddress = new Address();
                //candidateAddress.FullAddress = newOrderDto.Address.FullAddress;
                //candidateAddress.PostalCode = newOrderDto.Address.PostalCode;
                //candidateAddress.Floor = newOrderDto.Address.Floor;
                //candidateAddress.DoorbellName = newOrderDto.Address.DoorbellName;
                //candidateAddress.Type = AddressType.Customer;
                //candidateAddress.MarkNew();
                //candidate.Address = candidateAddress;

                decimal totalPrice = 0;

                foreach (var item in newOrderDto.OrderLines)
                {
                    OrderLines candidateOrderLines = new OrderLines();
                    candidateOrderLines.ProductId = item.ProductId;
                    candidateOrderLines.Quantity = item.Quantity;
                    candidateOrderLines.ProductPrice = candidateProducts.FirstOrDefault(x => x.Id == item.ProductId).Price;
                    totalPrice += item.Quantity * candidateProducts.FirstOrDefault(x => x.Id == item.ProductId).Price;
                    candidateOrderLines.Comments = item.Comments;
                    candidateOrderLines.MarkNew();
                    candidate.OrderLines.Add(candidateOrderLines);
                }

                candidate.TotalPrice = totalPrice;

                Order entity = await _orderRepository.AddNewOrder(candidate);
                var dtoAfterMapping = _orderMapper.Map<NewOrderResponseDto>(entity);
                response.SetSuccess(dtoAfterMapping);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing NewOrder with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing NewOrder with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }
        }
    }
}
