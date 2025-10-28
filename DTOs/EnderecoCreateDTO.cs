using System.ComponentModel.DataAnnotations;

namespace SistemaCliente.DTOs
{
    public class EnderecoCreateDTO
    {
        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "O CEP deve conter exatamente 8 números.")]
        public string Cep { get; set; } = string.Empty;

        [Required(ErrorMessage = "O número é obrigatório.")]
        public string Numero { get; set; } = string.Empty; 
        public string Complemento { get; set; } = string.Empty;
    }
}
