namespace SistemaCliente.DTOs
{
    public class EnderecoReadDTO
    {
        public string Cep { get; set; } = string.Empty;
        public string Logradouro { get; set; } 
        public string Cidade { get; set; } 
        public string Numero { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
    }
}
