using Application.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Service
{
    public interface IUserService
    {
        Task<bool> Register(UserForRegisterDto user, string password, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> Login(string username, string password, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> UserExists(string email, CancellationToken cancellationToken = default(CancellationToken));
    }
}
