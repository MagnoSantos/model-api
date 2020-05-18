using Microsoft.Extensions.Diagnostics.HealthChecks;
using sample.Infrastructure.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace sample.API.HealthChecks
{
    public class DummyAPIHealthCheck : IHealthCheck
    {
        private readonly IColaboradorService _colaboradorService;

        public DummyAPIHealthCheck(
            IColaboradorService colaboradorService
        )
        {
            _colaboradorService = colaboradorService;
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                var colaboradores = _colaboradorService.BuscarTodosOsColaboradores();

                if (colaboradores == null)
                {
                    return Task.FromResult(
                        HealthCheckResult.Unhealthy(
                            description: "API Dummy não retornou os colaboradores"
                        )
                    );
                }

                return Task.FromResult(HealthCheckResult.Healthy());
            }
            catch (Exception ex)
            {
                return Task.FromResult(
                    HealthCheckResult.Unhealthy(
                        description : "Falha ao obter todos os colaboradores",
                        exception: ex
                    )
                );
            }
        }
    }
}