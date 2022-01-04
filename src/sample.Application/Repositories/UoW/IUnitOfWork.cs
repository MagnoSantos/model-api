using System;
using System.Threading.Tasks;

namespace sample.Application.Repositories.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();
    }
}