using SistemaCliente.DTOs;

namespace SistemaCliente.Services
{
    public interface InterfaceViaCepService
    {
        Task<EnderecoCreateDTO?> BuscarEndereco(string cep);
    }
}
