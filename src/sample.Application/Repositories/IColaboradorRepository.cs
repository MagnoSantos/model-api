using sample.Application.DTO;
using sample.Domain.Entities;
using System.Threading.Tasks;

namespace sample.Application.Repositories.Interfaces
{
    public interface IColaboradorRepository
    {
        Task<Colaborador> AdicionarColaborador(ColaboradorDTO colaborador);
    }
}