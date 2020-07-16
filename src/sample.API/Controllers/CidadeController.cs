using Microsoft.AspNetCore.Mvc;
using sample.Infrastructure.Services;
using System.Net.Mime;
using System.Threading.Tasks;

namespace sample.API.Controllers
{
    [Route("api/cidades")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        private readonly ICidadeService _cidadeService;

        public CidadeController(
            ICidadeService cidadeService
        )
        {
            _cidadeService = cidadeService;
        }

        /// <summary>
        /// Buscar cidades.
        /// </summary>
        /// <param name="colaborador"></param>
        /// <returns>Colaborador que foi adicionado</returns>
        /// <response code="200">Retorna o colaborador que foi adicionado</response>
        /// <response code="400">O corpo da requisição está incorreto e/ou inválido</response>
        [HttpPost]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(Falha), 422)]
        public async Task<IActionResult> ObterPorUf(string uf)
        {
            var cidades = await _cidadeService.ObterPorUf(uf);

            if (cidades == null)
            {
                return NotFound();
            }

            return Ok(cidades);
        }
    }
}