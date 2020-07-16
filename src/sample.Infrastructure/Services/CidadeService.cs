using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using Polly;
using sample.Infrastructure.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sample.Infrastructure.Services
{
    public class CidadeService : ICidadeService
    {
        private readonly ConfiguracoesAplicacao _configuracoes;

        public CidadeService(
            IOptionsMonitor<ConfiguracoesAplicacao> config
        )
        {
            _configuracoes = config.CurrentValue;
        }

        public async Task<IEnumerable<CidadesResponse>> ObterPorUf(string uf)
        {
            return await Policy
                .Handle<FlurlHttpException>()
                .RetryAsync()
                .ExecuteAsync(() =>
                    _configuracoes.UrlApiLocalidades
                    .AppendPathSegments("api", "v1", "localidades", "estados", $"{uf}")
                    .PostJsonAsync(uf)
                    .ReceiveJson<IEnumerable<CidadesResponse>>()
            );
        }
    }
}