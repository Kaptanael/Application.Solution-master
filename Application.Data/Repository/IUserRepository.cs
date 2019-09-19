using System.Threading;
using System.Threading.Tasks;
using Application.Model;

namespace Application.Data.Repository
{
    public interface IUserRepository:IRepository<User>
    {    
        Task<User> Register(User user, string password, CancellationToken cancellationToken = default(CancellationToken));

        Task<User> Login(string username, string password, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> UserExists(string email, CancellationToken cancellationToken = default(CancellationToken));
    }
}
