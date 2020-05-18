using Newtonsoft.Json;

namespace sample.Infrastructure.Responses
{
    public class Dados
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Nome { get; set; }

        [JsonProperty("salary", NullValueHandling = NullValueHandling.Ignore)]
        public string Salario { get; set; }

        [JsonProperty("age", NullValueHandling = NullValueHandling.Ignore)]
        public string Idade { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
    }
}