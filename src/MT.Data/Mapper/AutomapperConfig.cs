using AutoMapper;
using MT.Domain.Dtos;
using MT.Domain.Entities;

namespace MT.Data.Mapper
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
           
            CreateMap<CaminhaoDetalheDto, Caminhao>();              
            CreateMap<Caminhao, CaminhaoDetalheDto>()
            .ForMember(desc => desc.Modelo, opt => opt.MapFrom(src => src.Modelo.Descricao));
            CreateMap<CaminhaoDetalheDto, Caminhao>()
           .ForMember(desc => desc.ModeloId, opt => opt.MapFrom(src => src.Modelo));

            CreateMap<Caminhao, CaminhaoDto>().ReverseMap();
            CreateMap<Modelo, ModeloDto>().ReverseMap();

        }
    }
}
