using AutoMapper;
using Data.Interfaces;
using Messages;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Models.Entities;
using Serilog;
using Services.Dtos;
using Services.Interfaces;

namespace Services.Services
{
    public class ProductService : IProductService
    {
        IProductRepository _repository;
        IMapper _mapper;

        public ProductService(
            IProductRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ObjectResponse<ProductDto>> GetActiveProduct(int id)
        {
            ObjectResponse<ProductDto> response = new ObjectResponse<ProductDto>();
            try
            {
                Product product = await _repository.GetActiveProductAsync(id);
                if (product == null) 
                {
                    response.SetHttpFailureCode($@"Product does not exist or is not active", HttpResultCode.NotFound);
                    return response;
                }

                var dtoAfterMapping = _mapper.Map<ProductDto>(product);

                response.SetSuccess(dtoAfterMapping);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing Get Product with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing Get Product with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }
        }

    }
}
