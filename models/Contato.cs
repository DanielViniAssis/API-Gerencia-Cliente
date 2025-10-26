using System.Collections.Generic;

namespace SistemaCliente.Models;

public class Contato
{
     public int Id { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string Texto { get; set; } = string.Empty;

// Relacionamentos entre cliente e contato
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }
}