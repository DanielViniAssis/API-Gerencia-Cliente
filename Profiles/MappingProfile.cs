using AutoMapper;
using SistemaCliente.Models;
using SistemaCliente.DTOs;
using SistemaCliente.Services;

namespace SistemaCliente.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Cliente
            CreateMap<ClienteCreateDTO, Cliente>();
            CreateMap<Cliente, ClienteReadDTO>();

            // Endereco
            CreateMap<EnderecoCreateDTO, Endereco>();
            CreateMap<Endereco, EnderecoReadDTO>();

            // Contato
            CreateMap<ContatoCreateDTO, Contato>();
            CreateMap<Contato, ContatoReadDTO>();

            // ViaCep - Endereco
            CreateMap<ViaCepResponse, Endereco>()
                .ForMember(dest => dest.Logradouro, opt => opt.MapFrom(src => src.Logradouro))
                .ForMember(dest => dest.Cidade, opt => opt.MapFrom(src => src.Localidade))
                .ForMember(dest => dest.Cep, opt => opt.MapFrom(src => src.Cep));
        }
    }
}
