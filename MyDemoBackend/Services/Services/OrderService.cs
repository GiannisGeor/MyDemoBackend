using AutoMapper;
using Data.Interfaces;
using FluentValidation;
using Messages;
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
        private readonly IMapper _orderMapper;
        private readonly IValidator<NewOrderDto> _newOrderValidator;

        public OrderService(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IMapper orderMapper,
            IValidator<NewOrderDto> newOrderValidator)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _orderMapper = orderMapper;
            _newOrderValidator = newOrderValidator;
        }

        public async Task<ObjectResponse<NewOrderResponseDto>> NewOrder(NewOrderDto newOrderDto)
        {
            ObjectResponse<NewOrderResponseDto> response = new();
            try
            {
                //response.SetHttpFailureCode($@"test", HttpResultCode.InternalServerError);
                //return response;
                var validationResult = _newOrderValidator.Validate(newOrderDto);
                if (!validationResult.IsValid)
                {
                    response.SetFailureWithValidation(validationResult);
                    return response;
                }

                List<Product> candidateProducts = await _productRepository.GetIdProducts(newOrderDto.OrderLines.Select(x => x.ProductId).Distinct().ToList());

                Order candidate = new Order();
                candidate.orderStatus = OrderStatus.OrderPlaced;
                candidate.StoreId = newOrderDto.StoreId;
                candidate.Name = newOrderDto.Name;
                candidate.OrderComments = newOrderDto.OrderComments;
                candidate.MarkNew();

                Address candidateAddress = new Address();
                candidateAddress.FullAddress = newOrderDto.Address.FullAddress;
                candidateAddress.PostalCode = newOrderDto.Address.PostalCode;
                candidateAddress.Phone = newOrderDto.Address.Phone;
                candidateAddress.Type = AddressType.Customer;
                candidateAddress.MarkNew();
                candidate.Address = candidateAddress;

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
