using System.ComponentModel.DataAnnotations;
using SistemaCliente.DTOs;
namespace SistemaCliente.DTOs;


public class ClienteCreateDTO
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O endereço é obrigatório.")]
    public EnderecoCreateDTO? Endereco { get; set; }

    [MinLength(1, ErrorMessage = "Deve possuir ao menos um contato.")]
    public List<ContatoCreateDTO> Contatos { get; set; } = new(); 
}