using Newtonsoft.Json;
using sample.Domain.Entities;

namespace sample.Infrastructure.Model
{
    public class AdicionarColaboradorResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public Dados Dados { get; set; }
    }
}