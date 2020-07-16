using System.Collections.Generic;

namespace sample.Infrastructure.Responses
{
    public class CidadesResponse
    {
        public IEnumerable<DadosCidade> Cidades { get; set; }
    }
}