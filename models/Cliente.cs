using System.Collections.Generic;

namespace SistemaCliente.Models;

public class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string DataCadastro { get; set; } = string.Empty;

    // Relacionamentos entre cliente - contato - endereço
    public Endereco? Endereco { get; set; }             
    public List<Contato> Contatos { get; set; } = new();
}