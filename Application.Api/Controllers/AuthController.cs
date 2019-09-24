using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Service;
using Application.ViewModel.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Application.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public AuthController(IUserService userService, IConfiguration configuration, ILogger<ValuesController> logger)
        {
            _userService = userService;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginRequest userForLoginRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }            

                var userToLogin =  await _userService.GenerateTokenAsync(userForLoginRequest.UserName.ToLower(), userForLoginRequest.Password, _configuration.GetSection("AppSettings:Token").Value);

                if (userToLogin == null)
                {
                    return Unauthorized();
                }

                return Ok(userToLogin);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterRequest userForRegisterRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await _userService.Register(userForRegisterRequest, userForRegisterRequest.Password);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
    }
}