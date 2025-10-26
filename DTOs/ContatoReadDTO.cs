namespace SistemaCliente.DTOs
{
    public class ContatoReadDTO
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Texto { get; set; } = string.Empty;
    }
}
