
namespace SistemaCliente.Services
{
    public interface InterfaceViaCepService
    {
        Task<ViaCepResponse?> BuscarEndereco(string cep);
    }
}
