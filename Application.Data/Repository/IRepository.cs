using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        TEntity Update(TEntity entity);

        TEntity Delete(TEntity entity);        

        Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));        
    }
}
