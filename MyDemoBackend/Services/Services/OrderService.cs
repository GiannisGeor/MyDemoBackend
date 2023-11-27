using System.Collections.Immutable;
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
        private readonly IOptionRepository _optionRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _orderMapper;
        private readonly IValidator<NewOrderDto> _newOrderValidator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IOptionRepository optionRepository,
            IAddressRepository addressRepository,
            IMapper orderMapper,
            IHttpContextAccessor httpContextAccessor,
            IValidator<NewOrderDto> newOrderValidator)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _optionRepository = optionRepository;
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

                List<Options> candidateOptions = await _optionRepository.GetIdOptions(newOrderDto.OrderLines.SelectMany(x => x.OrderLinesOptions.Select(o => o.OptionsId)).Distinct().ToList());

                Order candidate = new Order();
                candidate.orderStatus = OrderStatus.OrderPlaced;
                candidate.StoreId = newOrderDto.StoreId;
                candidate.AddressId = newOrderDto.AddressId;
                candidate.CustomerId = customerId;
                candidate.OrderComments = newOrderDto.OrderComments;
                candidate.MarkNew();

                decimal totalPrice = 0;

                foreach (var orderLineDto in newOrderDto.OrderLines)
                {
                    OrderLines candidateOrderLine = new OrderLines();
                    candidateOrderLine.ProductId = orderLineDto.ProductId;
                    candidateOrderLine.Quantity = orderLineDto.Quantity;
                    candidateOrderLine.ProductPrice = candidateProducts.FirstOrDefault(x => x.Id == orderLineDto.ProductId).Price;
                    totalPrice += orderLineDto.Quantity * candidateOrderLine.ProductPrice;
                    candidateOrderLine.Comments = orderLineDto.Comments;
                    candidateOrderLine.MarkNew();

                    foreach (var option in orderLineDto.OrderLinesOptions)
                    {
                        OrderLinesOptions candidateOrderLineOption = new OrderLinesOptions();
                        candidateOrderLineOption.OptionsId = option.OptionsId;
                        candidateOrderLineOption.OptionExtraCost = candidateOptions.FirstOrDefault(x => x.Id == option.OptionsId).ExtraCost;
                        totalPrice += candidateOrderLineOption.OptionExtraCost;
                        candidateOrderLineOption.MarkNew();
                        candidateOrderLine.OrderLinesOptions.Add(candidateOrderLineOption);
                    }
                    candidate.OrderLines.Add(candidateOrderLine);
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
