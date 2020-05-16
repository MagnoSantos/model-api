using Microsoft.AspNetCore.Mvc;
using sample.API.Respostas;
using sample.Application;
using sample.Application.DTO;
using sample.Application.Repositories.Interfaces;
using System.Net.Mime;
using System.Threading.Tasks;

namespace sample.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class ColaboradorController : ControllerBase
    {
        private readonly IColaboradorRepository _colaboradorRepository;

        public ColaboradorController(
            IColaboradorRepository colaboradorRepository
        )
        {
            _colaboradorRepository = colaboradorRepository;
        }

        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(Sucesso<ColaboradorResposta>), 200)]
        [ProducesResponseType(typeof(Falha), 422)]
        public async Task<IActionResult> CadastrarColaborador([FromBody]Colaborador colaborador)
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

            return Ok(
                new Sucesso<ColaboradorResposta>(
                    new ColaboradorResposta 
                    {
                        data = colaboradorAdicionado.data,
                        status = colaboradorAdicionado.status 
                    }
                )
            );
        }
    }
}