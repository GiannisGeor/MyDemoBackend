using AutoMapper;
using Models.Projections;
using Services.Dtos;
using Models.Entities;


namespace Api.Helpers
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<StoreStoreCategoryAddressProjection, StoreStoreCategoryAddressDto>().ReverseMap();
            CreateMap<AddressProjection, AddressDto>().ReverseMap();
            CreateMap<StoreCategoryProjection, StoreCategoryDto>().ReverseMap();
            CreateMap<AllInitialDataProjection, AllInitialDataDto>().ReverseMap();
            CreateMap<ProductCategoryProjection, ProductCategoryDto>().ReverseMap();
            CreateMap<ProductProjection, ProductDto>().ReverseMap();
            CreateMap<Order, NewOrderResponseDto>();
            CreateMap<Address, AddressResponseDto>();
            CreateMap<OrderLines, NewOrderLinesResponseDto>();
        }
    }
}
