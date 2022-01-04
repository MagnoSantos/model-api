using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace sample.Application.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly DataContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<TEntity>();
        }

        public void Add(TEntity entity) => _dbSet.Add(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.AsNoTracking().ToListAsync();


        public async Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> predicate) 
            => await _dbSet.Where(predicate).AsNoTracking().ToListAsync();

        public async Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> predicate, bool tracking)
            => await (tracking ? _dbSet.Where(predicate).AsNoTracking().ToListAsync() : _dbSet.Where(predicate).AsNoTracking().ToListAsync());

        public async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate)
            => await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);

        public async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate, bool tracking)
            => await (tracking ? _dbSet.FirstOrDefaultAsync(predicate) : _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate));

        public IQueryable<TEntity> Query() => _dbSet.AsQueryable<TEntity>();

        public void Remove(TEntity entity) => _dbSet.Remove(entity);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Update(TEntity entity) => _context.Entry(entity).State = EntityState.Modified;
    }

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        //Incluir DBSets
        //Exemplo: public virtual DbSet<Foo> Foo { get; set; }

        //Incluir Mappings
        /* Exemplo:
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FooMapping());
            base.OnModelCreating(modelBuilder);
        }
        
        Mapping:
        public void Configure(EntityTypeBuilder<FooEntity> builder)
        {
            builder.HasKey(e => new { e.BillingAgreementUuid, e.AccountUuid, e.BillingAgreementDetailNu })
                .IsClustered(false);
        }
        */
    }
}