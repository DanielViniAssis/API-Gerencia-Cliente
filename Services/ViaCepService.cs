using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
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

        public async Task<ViaCepResponse?> BuscarEndereco(string cep)
        {
            Console.WriteLine($"Buscando endereço para o CEP: {cep}");
            Console.WriteLine($"https://viacep.com.br/ws/{cep}/json/");
            // Remove caracteres não numéricos
            cep = new string(cep.Where(char.IsDigit).ToArray());

            if (string.IsNullOrWhiteSpace(cep))
                return null;

            var url = $"https://viacep.com.br/ws/{cep}/json/";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            // Lê o JSON retornado pela API
            var dados = await response.Content.ReadFromJsonAsync<ViaCepResponse>();

            if (dados == null || dados.Erro)
                return null;

            // Converte a resposta do ViaCep em enderecoDTO
            // var endereco = new EnderecoCreateDTO
            // {
            //     Cep = dados.Cep ?? "",
            //     // Logradouro  = dados.Logradouro  ?? "",
            //     // Cidade = dados.Localidade ?? "",
            //     Numero = "", // o usuário vai digitar
            //     Complemento = dados.Complemento ?? ""
            // };

            return dados;
        }
    }

    public class ViaCepResponse
    {
        [JsonPropertyName("cep")]
        public string Cep { get; set; } = string.Empty;
        [JsonPropertyName("logradouro")]
        public string Logradouro  { get; set; } = string.Empty;
        [JsonPropertyName("complemento")]
        public string Complemento { get; set; } = string.Empty;
        
        [JsonPropertyName("localidade")]
        public string Localidade { get; set; } = string.Empty; // Cidade
        public bool Erro { get; set; } // vem como "erro": true quando o CEP não existe
    }
}
