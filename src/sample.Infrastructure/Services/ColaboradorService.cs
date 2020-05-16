using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using Polly;
using sample.Infrastructure.Responses;
using System.Threading.Tasks;

namespace sample.Infrastructure.Services
{
    public class ColaboradorService : IColaboradorService
    {
        private readonly ConfiguracoesAplicacao _config;

        public ColaboradorService(
            IOptionsMonitor<ConfiguracoesAplicacao> config
        )
        {
            _config = config.CurrentValue;
        }

        /// <summary>
        /// Método de adição de colaborador.
        /// Utilização de política de retry com polly, objeto anônimo para construção do JSON.
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="salario"></param>
        /// <param name="idade"></param>
        /// <returns></returns>
        public async Task<AdicionarColaboradorResponse> AdicionarColaborador(
            string nome,
            string salario,
            string idade
        )
        {
            return await Policy
                .Handle<FlurlHttpException>()
                .RetryAsync()
                .ExecuteAsync(() =>
                    _config.UrlDummy
                    .AppendPathSegments("api", "v1", "create")
                    .PostJsonAsync(new
                    {
                        name = $"{nome}",
                        salario = $"{salario}",
                        idade = $"{idade}"
                    })
                    .ReceiveJson<AdicionarColaboradorResponse>()
            );
        }
    }
}