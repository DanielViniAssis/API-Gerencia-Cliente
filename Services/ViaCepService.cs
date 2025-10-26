using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SistemaCliente.DTOs;

namespace SistemaCliente.Services
{
    public class ViaCepService : InterfaceViaCepService
    {
        private readonly HttpClient _httpClient;

        public ViaCepService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EnderecoCreateDTO?> BuscarEndereco(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
                return null;

            // Remove caracteres não numéricos
            cep = new string(cep.Where(char.IsDigit).ToArray());

            var url = $"https://viacep.com.br/ws/{cep}/json/";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            // Lê o JSON retornado pela API
            var dados = await response.Content.ReadFromJsonAsync<ViaCepResponse>();

            // Se a API indicar erro (CEP inexistente)
            if (dados == null || dados.Erro)
                return null;

            // Converte a resposta do ViaCep em nosso DTO de endereço
            var endereco = new EnderecoCreateDTO
            {
                Cep = dados.Cep ?? "",
                Logradouro = dados.Logradouro ?? "",
                Cidade = dados.Localidade ?? "",
                Numero = "", // o usuário vai digitar
                Complemento = dados.Complemento ?? ""
            };

            return endereco;
        }
    }

    public class ViaCepResponse
    {
        public string Cep { get; set; } = string.Empty;
        public string Logradouro { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Localidade { get; set; } = string.Empty; // Cidade
        public string Uf { get; set; } = string.Empty;
        public string Ibge { get; set; } = string.Empty;
        public string Gia { get; set; } = string.Empty;
        public string Ddd { get; set; } = string.Empty;
        public string Siafi { get; set; } = string.Empty;
        public bool Erro { get; set; } // vem como "erro": true quando o CEP não existe
    }
}
