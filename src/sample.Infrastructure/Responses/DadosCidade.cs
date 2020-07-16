using Flurl;
using System.Text.Json.Serialization;

namespace sample.Infrastructure.Responses
{
    public class DadosCidade
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("sigla")]
        public string Sigla { get; set; }

        [JsonPropertyName("regiao")]
        public dynamic Regiao { get; set; }
    }
}