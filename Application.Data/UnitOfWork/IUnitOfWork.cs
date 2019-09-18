using Application.Data.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ITaskRepository Tasks { get; }
        IValueRepository Values { get; }
        int Save();
        Task<int> SaveAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
