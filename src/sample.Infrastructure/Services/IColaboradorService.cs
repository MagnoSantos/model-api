using sample.Infrastructure.Responses;
using System.Threading.Tasks;

namespace sample.Infrastructure.Services
{
    public interface IColaboradorService
    {
        Task<AdicionarColaboradorResponse> AdicionarColaborador(string nome, string salario, string idade);

        Task<BuscarColaboradoresResponse> BuscarTodosOsColaboradores();
    }
}