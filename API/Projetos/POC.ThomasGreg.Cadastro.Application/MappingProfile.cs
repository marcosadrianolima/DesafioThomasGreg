using AutoMapper;
using POC.ThomasGreg.Cadastro.Application.DTO;
using POC.ThomasGreg.Cadastro.Domain.Features.Cliente.Entidades;
using POC.ThomasGreg.Cadastro.Domain.Features.Logradouro.Entidade;

namespace POC.ThomasGreg.Cadastro.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClienteVO, ClienteDTO>()
            .ForMember(dest => dest.Logradouros, opt => opt.MapFrom(src => src.Logradouros));

            CreateMap<ClienteDTO, ClienteVO>()
                .ForMember(dest => dest.Logradouros, opt => opt.MapFrom(src => src.Logradouros));

            CreateMap<LogradouroVO, LogradouroDTO>();
            CreateMap<LogradouroDTO, LogradouroVO>();
        }
    }
}
