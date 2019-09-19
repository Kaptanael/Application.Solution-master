using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Data.UnitOfWork;
using Application.Model;
using Application.ViewModel.User;
using AutoMapper;

namespace Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<bool> Login(string username, string password, CancellationToken cancellationToken = default(CancellationToken))
        {
            var userToLogin = await _uow.Users.Login(username, password);
            return true;
        }

        public async Task<bool> Register(UserForRegisterDto userForRegisterDto, string password, CancellationToken cancellationToken = default(CancellationToken))
        {
            var userToRegistered = _mapper.Map<User>(userForRegisterDto);
            await _uow.Users.Register(userToRegistered, password, cancellationToken);
            return true;
        }

        public async Task<bool> UserExists(string email, CancellationToken cancellationToken = default(CancellationToken))
        {
           return await _uow.Users.UserExists(email, cancellationToken);            
        }
    }
}
