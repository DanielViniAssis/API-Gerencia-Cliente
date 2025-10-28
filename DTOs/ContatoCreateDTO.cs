using System.ComponentModel.DataAnnotations;

namespace SistemaCliente.DTOs
{
    public class ContatoCreateDTO
    {
        [Required(ErrorMessage = "O tipo de contato é obrigatório.")]
        [StringLength(50, ErrorMessage = "O tipo deve ter no máximo 50 caracteres.")]
        public string Tipo { get; set; } = string.Empty;
        
    [Required(ErrorMessage = "O texto do contato é obrigatório.")]
    [StringLength(100, ErrorMessage = "O texto deve ter no máximo 100 caracteres.")]
        public string Texto { get; set; } = string.Empty;
    }
}
