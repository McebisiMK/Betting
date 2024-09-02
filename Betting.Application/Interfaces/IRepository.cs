using System.Linq.Expressions;

namespace Betting.Application.Interfaces
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> GetAll();
        bool Exists(Expression<Func<TEntity, bool>> expression);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
