using Newtonsoft.Json;

namespace sample.Infrastructure.Responses
{
    public class AdicionarColaboradorResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public Data Dados { get; set; }
    }
}