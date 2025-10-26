using SistemaCliente.DTOs;
namespace SistemaCliente.DTOs;


public class ClienteCreateDTO
{
    public string Nome { get; set; } = string.Empty;
    public string DataCadastro { get; set; } = string.Empty;
    public EnderecoCreateDTO? Endereco { get; set; }
    public List<ContatoCreateDTO> Contatos { get; set; } = new(); 
}