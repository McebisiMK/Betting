using Betting.Application.Interfaces;
using Betting.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Betting.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly BettingDbContext _bettingDbContext;

        public Repository(BettingDbContext bettingDbContext)
        {
            _bettingDbContext = bettingDbContext;
            _dbSet = _bettingDbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
        {
            return await _dbSet.CountAsync(expression, cancellationToken);
        }

        public bool Exists(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Any(expression);
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _bettingDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
