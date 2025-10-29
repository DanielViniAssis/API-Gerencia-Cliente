using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaCliente.Models;

public class Cliente
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    public string DataCadastro { get; set; } = string.Empty;

    // Relacionamentos entre cliente - contato - endereço
    public Endereco? Endereco { get; set; }
    public List<Contato> Contatos { get; set; } = new();
    
}