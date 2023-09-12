using AutoMapper;
using Models.Entities;
using Models.Projections;
using Services.Dtos;

namespace Api.Helpers
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Pelatis, PelatisDto>().ReverseMap();
            CreateMap<Enoikiasi, EnoikiasiDto>().ReverseMap();
            CreateMap<EpistrofiKasetasProjection, EpistrofiKasetasDto>()
                .ForMember(dest => dest.IdKasetass, opt => opt.MapFrom(src => src.IdKasetas))
                .ForMember(dest => dest.ImerominiaEpistrofiss, opt => opt.MapFrom(src => src.ImerominiaEpistrofis));
            CreateMap<StoixeiaPelatiKaiEnoikiasisProjection, StoixeiaPelatiKaiEnoikiasisDto>().ReverseMap();
            CreateMap<KasetesTimesProjection, KasetesTimesDto>().ReverseMap();
            CreateMap<OnomaKaiRolosSintelestiProjection, OnomaKaiRolosSintelestiDto>().ReverseMap();
        }
    }
}
