using SistemaCliente.DTOs;
namespace SistemaCliente.DTOs;

public class ClienteReadDTO
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string DataCadastro { get; set; } = string.Empty;
    public EnderecoReadDTO? Endereco { get; set; }
    public List<ContatoReadDTO> Contatos { get; set; } = new();
}
