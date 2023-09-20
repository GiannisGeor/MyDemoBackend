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
            CreateMap<NeaTainiaDto, Tainia>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Kasetes, opt => opt.Ignore())
                .ForMember(x => x.TainiesSintelestes, opt => opt.Ignore())
                .ForMember(x => x.IsActive, opt => opt.Ignore());
            CreateMap<Tainia, NeaTainiaResponseDto>();
            //CreateMap<MetaboliTainiasDto, Tainia>()
            //    .ForMember(x => x.Id, opt => opt.Ignore())
            //    .ForMember(x => x.Kasetes, opt => opt.Ignore())
            //    .ForMember(x => x.TainiesSintelestes, opt => opt.Ignore())
            //    .ForMember(x => x.IsActive, opt => opt.Ignore());
            CreateMap<Tainia, MetaboliTainiasResponseDto>();
            CreateMap<Tainia, NeaTainiaKaiSintelestesResponseDto>();
            CreateMap<Tn_sn, SintelestesKaiTn_snResponseDto>();
            CreateMap<Sintelestis, SintelestisResponseDto>();
            CreateMap<Tainia, ProsthikiSintelestonResponseDto>();
            CreateMap<NeosPelatisDto, Pelatis>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.IsActive, opt => opt.Ignore())
                .ForMember(x => x.Enoikiasis, opt => opt.Ignore());
            CreateMap<Pelatis, NeosPelatisResponseDto>().ReverseMap();
            CreateMap<Kaseta, NeaKasetaResponseDto>();
            CreateMap<Tainia, OnomaTainiasResponseDto>();

        }
    }
}
