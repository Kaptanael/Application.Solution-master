using Application.Model;
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
        Task<bool> Register(UserForRegisterRequest user, string password, CancellationToken cancellationToken = default(CancellationToken));        

        Task<bool> UserExists(string email, CancellationToken cancellationToken = default(CancellationToken));

        Task<UserForLoginResponse> GenerateTokenAsync(string username, string password, string secrectKey);
    }
}
