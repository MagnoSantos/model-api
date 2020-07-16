using sample.Domain.Entities;
using sample.Infrastructure.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sample.Infrastructure.Services
{
    public interface ICidadeService
    {
        Task<IEnumerable<CidadesResponse>> ObterPorUf(string uf);
    }
}