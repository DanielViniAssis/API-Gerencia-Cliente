namespace SistemaCliente.DTOs
{
    public class EnderecoCreateDTO
    {
        public string Cep { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty; 
        public string Complemento { get; set; } = string.Empty;
    }
}
