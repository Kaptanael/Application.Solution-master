using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Data.UnitOfWork;
using Application.Model;
using Application.ViewModel.User;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;

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
        public async Task<User> UserExists(string username, string password, CancellationToken cancellationToken = default(CancellationToken))
        {
            var userToLogin = await _uow.Users.Login(username, password);            
            return userToLogin;
        }

        public async Task<bool> Register(UserForRegisterDto userForRegisterDto, string password, CancellationToken cancellationToken = default(CancellationToken))
        {
            var userToRegistered = new User
            {
                UserName = userForRegisterDto.Username
            };
            await _uow.Users.Register(userToRegistered, password, cancellationToken);
            return true;
        }

        public async Task<bool> UserExists(string email, CancellationToken cancellationToken = default(CancellationToken))
        {
           return await _uow.Users.UserExists(email, cancellationToken);            
        }

        public object Login(User user,string secrectKey)
        {
            var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.UserName)
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secrectKey));

            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credential
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new UserForAuthDto
            {
                Token = tokenHandler.WriteToken(token),
                Id = user.Id.ToString(),
                UserName = user.UserName
            };
        }
    }
}
