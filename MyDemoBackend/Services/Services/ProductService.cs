using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data.Interfaces;
using Data.Repositories;
using Messages;
using Serilog;
using Services.Dtos;
using Services.Interfaces;

namespace Services.Services
{
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;
        IMapper _productMapper;

        public ProductService(
            IProductRepository productRepository,
            IMapper productMapper)
        {
            _productRepository = productRepository;
            _productMapper = productMapper;
        }

        public async Task<ObjectResponse<AllProductDataDto>> GetAllProductData(int id)
        {
            ObjectResponse<AllProductDataDto> response = new();
            try
            {
                var AllProductData = await _productRepository.GetAllProductData(id);
                var dtoAfterMapping = _productMapper.Map<AllProductDataDto>(AllProductData);
                response.SetSuccess(dtoAfterMapping);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $@"Error while executing GetStoreStoreCategoryAddress with message : {e.Message} ");
                response.SetHttpFailureCode($@"Error while executing GetStoreStoreCategoryAddress with message : {e.Message}", HttpResultCode.InternalServerError);
                return response;
            }
        }
    }
}
