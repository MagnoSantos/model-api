using Microsoft.AspNetCore.Mvc;
using sample.Application;
using sample.Application.DTO;
using sample.Application.Repositories.Interfaces;
using System.Net.Mime;
using System.Threading.Tasks;

namespace sample.API.Controllers
{
    [Route("api/colaborador")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    public class ColaboradorController : ControllerBase
    {
        private readonly IColaboradorRepository _colaboradorRepository;

        public ColaboradorController(
            IColaboradorRepository colaboradorRepository
        )
        {
            _colaboradorRepository = colaboradorRepository;
        }

        /// <summary>
        /// Adiciona um novo colaborador via API.
        /// </summary>
        /// <param name="colaborador"></param>
        /// <returns>Colaborador que foi adicionado</returns>
        /// <response code="200">Retorna o colaborador que foi adicionado</response>
        /// <response code="400">O corpo da requisição está incorreto e/ou inválido</response>
        /// <response code="422">Colaborador não foi retornado após o cadastro</response>
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(Falha), 422)]
        public async Task<IActionResult> CadastrarColaborador([FromBody]ColaboradorDTO colaborador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var colaboradorAdicionado = await _colaboradorRepository.AdicionarColaborador(colaborador);

            if (colaboradorAdicionado == null)
            {
                return UnprocessableEntity(new Falha(Constantes.Mensagens.ErroProcessamento));
            }

            return Ok(colaboradorAdicionado);
        }
    }
}