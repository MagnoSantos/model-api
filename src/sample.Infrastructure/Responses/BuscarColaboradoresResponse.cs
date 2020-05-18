using Newtonsoft.Json;
using System.Collections.Generic;

namespace sample.Infrastructure.Responses
{
    public class BuscarColaboradoresResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public IEnumerable<dynamic> Dados { get; set; }
    }
}