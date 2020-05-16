using sample.Application.Repositories.Interfaces;
using sample.Domain.Entities;
using sample.Infrastructure.Services;
using System.Threading.Tasks;

namespace sample.Application.Repositories
{
    public class ColaboradorRepository : IColaboradorRepository
    {
        private readonly IColaboradorService _colaboradorService;

        public ColaboradorRepository(
            IColaboradorService colaboradorService
        )
        {
            _colaboradorService = colaboradorService;
        }

        public async Task<Colaborador> AdicionarColaborador(DTO.Colaborador colaborador)
        {
            var resposta =
                await _colaboradorService.AdicionarColaborador(
                    colaborador.Nome,
                    colaborador.Salario,
                    colaborador.Idade
                );

            if (resposta == null)
            {
                return null;
            }

            return new Colaborador(Status: resposta.Status, Data: resposta.Dados);
        }
    }
}