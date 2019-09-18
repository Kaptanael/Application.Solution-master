using System.Threading;
using System.Threading.Tasks;
using Application.Model;

namespace Application.Data.Repository
{
    public interface IValueRepository: IRepository<Value>
    {
        Task<bool> IsDuplicateAsync(string name, CancellationToken cancellationToken = default(CancellationToken));
    }
}
