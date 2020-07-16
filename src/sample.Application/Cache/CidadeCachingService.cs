using Microsoft.Extensions.Caching.Memory;
using sample.Infrastructure.Responses;
using sample.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sample.Application.Cache
{
    public class CidadeCachingService : ICidadeService
    {
        private readonly IMemoryCache _cache;
        private readonly ICidadeService _decoratedService;

        public CidadeCachingService(
            IMemoryCache cache,
            ICidadeService decoratedService
        )
        {
            _cache = cache;
            _decoratedService = decoratedService;
        }

        public async Task<IEnumerable<CidadesResponse>> ObterPorUf(string uf)
        {
            var chave = $"chave_{uf}";

            if (_cache.TryGetValue(chave, out IEnumerable<CidadesResponse?> cidade))
            {
                return cidade;
            }

            var cidades = await _decoratedService.ObterPorUf(uf);

            if (cidades == null) return null;

            return _cache.Set(
                chave,
                cidades,
                //Tempo de expiração
                new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
                }
            );
        }
    }
}