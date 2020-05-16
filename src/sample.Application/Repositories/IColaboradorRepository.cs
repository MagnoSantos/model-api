using System.Threading.Tasks;

namespace sample.Application.Repositories.Interfaces
{
    public interface IColaboradorRepository
    {
        Task<Domain.Entities.Colaborador> AdicionarColaborador(DTO.Colaborador colaborador);
    }
}