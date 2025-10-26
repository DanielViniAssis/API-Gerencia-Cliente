using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaCliente.Models;

public class Endereco
{   
    [Key] // chave primária compartilhada com Cliente
    [ForeignKey(nameof(Cliente))]
    [DatabaseGenerated(DatabaseGeneratedOption.None)] // Desativa geração automática
    public int ClienteId { get; set; }    //Chave primária e estrangeira
    public string Cep { get; set; } = string.Empty;
    public string Logadouro { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string Complemento { get; set; } = string.Empty;

    // Relacionamentos entre cliente e endereço
    public Cliente? Cliente { get; set; }
}