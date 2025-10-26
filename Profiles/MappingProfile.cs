using AutoMapper;
using SistemaCliente.Models;
using SistemaCliente.DTOs;

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
        }
    }
}
