using AutoMapper;
using GeoSense.API.DTOs;
using GeoSense.API.Infrastructure.Persistence;
using GeoSense.API.Domain.Enums;

namespace GeoSense.API.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Moto, MotoDetalhesDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Modelo, opt => opt.MapFrom(src => src.Modelo))
                .ForMember(dest => dest.Placa, opt => opt.MapFrom(src => src.Placa))
                .ForMember(dest => dest.Chassi, opt => opt.MapFrom(src => src.Chassi))
                .ForMember(dest => dest.ProblemaIdentificado, opt => opt.MapFrom(src => src.ProblemaIdentificado))
                .ForMember(dest => dest.VagaId, opt => opt.MapFrom(src => src.VagaId));

            CreateMap<Vaga, VagaDTO>()
                .ForMember(dest => dest.Numero, opt => opt.MapFrom(src => src.Numero))
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => (int)src.Tipo))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status))
                .ForMember(dest => dest.PatioId, opt => opt.MapFrom(src => src.PatioId));

            CreateMap<Patio, PatioDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Senha, opt => opt.MapFrom(src => src.Senha))
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => (int)src.Tipo));

            CreateMap<UsuarioDTO, Usuario>()
                .ConstructUsing(dto => new Usuario(dto.Id, dto.Nome, dto.Email, dto.Senha, (TipoUsuario)dto.Tipo));
        }
    }
}