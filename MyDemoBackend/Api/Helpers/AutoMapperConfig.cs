using AutoMapper;
using Models.Projections;
using Services.Dtos;

namespace Api.Helpers
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<StoreStoreCategoryAddressProjection, StoreStoreCategoryAddressDto>().ReverseMap();
            CreateMap<AddressProjection, AddressDto>().ReverseMap();
            CreateMap<StoreCategoryProjection, StoreCategoryDto>().ReverseMap();
        }
    }
}
