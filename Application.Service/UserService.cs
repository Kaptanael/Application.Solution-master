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

        public async Task<bool> Register(UserForRegisterRequest userForRegisterRequest, string password, CancellationToken cancellationToken = default(CancellationToken))
        {
            var userToRegistered = new User
            {
                UserName = userForRegisterRequest.Username
            };
            await _uow.Users.Register(userToRegistered, password, cancellationToken);
            return true;
        }

        public async Task<bool> UserExists(string email, CancellationToken cancellationToken = default(CancellationToken))
        {
           return await _uow.Users.UserExists(email, cancellationToken);            
        }

        public async Task<UserForLoginResponse> GenerateTokenAsync(string username, string password, string secrectKey)
        {
            var userToLogin = await _uow.Users.Login(username, password);

            if (userToLogin == null)
            {
                return null;
            }

            var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userToLogin.Id.ToString()),
                    new Claim(ClaimTypes.Name,userToLogin.UserName)
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

            return new UserForLoginResponse
            {
                Token = tokenHandler.WriteToken(token),
                Id = userToLogin.Id.ToString(),
                UserName = userToLogin.UserName
            };
        }        
    }
}
