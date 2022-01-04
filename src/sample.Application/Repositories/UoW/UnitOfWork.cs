using System;
using System.Threading.Tasks;

namespace sample.Application.Repositories.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception saveChangesException)
            {
                try
                {
                    await _context.Database.RollbackTransactionAsync();
                }
                catch (Exception rollBackException)
                {
                    throw new AggregateException(new[] { saveChangesException, rollBackException });
                }
                throw;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}